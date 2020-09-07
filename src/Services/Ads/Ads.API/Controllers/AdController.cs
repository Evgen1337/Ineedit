using Ads.API.Application.Commands;
using Ads.API.Application.Exceptions;
using Ads.API.Application.Queries;
using Ads.API.Application.ViewModels;
using Ads.Dtos.Ad;
using IdentityLib;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ads.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IAdQueries _adQueries;

        public AdController(IMediator mediator, IIdentityService identityService, IAdQueries adQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _adQueries = adQueries ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(CollectionContainer<AdViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAdsAsync()
        {
            var userId = _identityService.GetUserIdentity();
            var ads = await _adQueries.GetUserAdsAsync(Guid.Parse(userId));

            return Ok(ads.ToCollectionContainer());
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAdAsync([FromQuery] GettingAdDto gettingAdDto)
        {
            try
            {
                var ad = await _adQueries.GetAdAsync(gettingAdDto.AdId);

                return Ok(ad);
            }
            catch (AdNotFoundException ex)
            {
                return NotFound(ex.ToErrorsContainer());
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(AdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAdAsync([FromBody] UpdatingAdDto updatingAdDto)
        {
            var userId = _identityService.GetUserIdentity();
            var command = new UpdateAdCommand(updatingAdDto, Guid.Parse(userId));

            try
            {
                var updatedAd = await _mediator.Send(command);
                return Ok(updatedAd);
            }
            catch (UserWithAdOwnerDoesntEqualsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddAdAsync([FromBody] CreatingAdDto creatingAdDto)
        {
            var userId = _identityService.GetUserIdentity();
            var command = new CreateAdCommand(Guid.Parse(userId), creatingAdDto);

            try
            {
                var ad = await _mediator.Send(command);
                return Ok(ad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(AdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync([FromQuery] DeletingAdDto deletingAdDto)
        {
            var userId = _identityService.GetUserIdentity();
            var command = new DeleteAdCommand(Guid.Parse(userId), deletingAdDto.AdId);

            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (AdNotFoundException ex)
            {
                return NotFound(ex.ToErrorsContainer());
            }
            catch (UserWithAdOwnerDoesntEqualsException ex)
            {
                return BadRequest(ex.ToErrorsContainer());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToErrorsContainer());
            }
        }
    }
}
