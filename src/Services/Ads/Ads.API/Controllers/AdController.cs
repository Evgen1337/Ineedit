using Ads.API.Application.Commands;
using Ads.API.Application.Exceptions;
using Ads.API.Application.Queries;
using Ads.API.Application.ViewModels;
using Ads.API.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AdController> _logger;

        public AdController(IMediator mediator, IIdentityService identityService, IAdQueries adQueries, ILogger<AdController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(mediator));
            _adQueries = adQueries ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAdAsync([FromQuery] GettingAdViewModel gettingAdViewModel)
        {
            try
            {
                var ad = await _adQueries.GetAdAsync(gettingAdViewModel.AdId);

                return Ok(ad);
            }
            catch (AdNotFoundException ex)
            {
                return NotFound(ex.ToErrorsContainer());
            }
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

        [HttpPut]
        [ProducesResponseType(typeof(AdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAdAsync([FromBody] UpdatingAdViewModel updateAdViewModel)
        {
            var userId = _identityService.GetUserIdentity();
            var command = new UpdateAdCommand(updateAdViewModel, Guid.Parse(userId));

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
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddAdAsync([FromBody] CreatingAdViewModel adViewModel)
        {
            var userId = _identityService.GetUserIdentity();
            var command = new CreateAdCommand(Guid.Parse(userId), adViewModel);

            try
            {
                var ad = await _mediator.Send(command);
                return Ok(ad);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(AdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsContainer), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAdAsync([FromQuery] DeletingAdViewModel deletingAdView)
        {
            var userId = _identityService.GetUserIdentity();
            var command = new DeleteAdCommand(Guid.Parse(userId), deletingAdView.AdId);

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
            catch(Exception ex)
            {
                return BadRequest(ex.ToErrorsContainer());
            }
        }
    }
}
