﻿using Identity.API.Application;
using Identity.API.Application.Exceptions;
using Identity.API.Application.ViewModels;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager, ILogger<AccountController> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((typeof(ErrorsContainer)), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
                NormalizedEmail = model.Email,
                NormalizedUserName = model.Email,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded
                ? (IActionResult)Ok()
                : BadRequest(result.Errors.ToErrorsContainer());
        }

        [HttpPost]
        [Route("authenticate")]
        [ProducesResponseType((typeof(AuthenticateViewModel)), (int)HttpStatusCode.OK)]
        [ProducesResponseType((typeof(ErrorsContainer)), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            try
            {
                if (user is null)
                    throw new InvalidLoginOrPassException("Invalid login or password");

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!isPasswordValid)
                    throw new InvalidLoginOrPassException("Invalid login or password");

                var claimsIdentity = await GetClaimsIdentityAsync(user, model.Password);
                var jwt = JwtHelper.GenerateJwt(claimsIdentity);

                return Ok(new AuthenticateViewModel(user.Id, user.Email, jwt));
            }
            catch (InvalidLoginOrPassException ex)
            {
                return BadRequest(ex.ToErrorsContainer());
            }
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityAsync(IdentityUser user, string password)
        {
            var userToVerify = await _userManager.FindByEmailAsync(user.Email);

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                var claims = await _userManager.GetClaimsAsync(userToVerify);
                var claimsIdentity = GenerateClaimsIdentity(user, claims);

                return claimsIdentity;
            }

            return null;
        }

        private ClaimsIdentity GenerateClaimsIdentity(IdentityUser user, IList<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));

            var claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }
    }
}
