using Ads.API.Application.Exceptions;
using Ads.API.Application.ViewModels;
using Ads.Domain.AggregatesModel.AdAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ads.API.Application.Commands
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, AdViewModel>
    {
        private readonly IAdRepository _adRepository;

        public UpdateAdCommandHandler(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public async Task<AdViewModel> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
            var ad = await _adRepository.GetByIdAsync(request.UpdatingAdDto.AdId);

            if (ad is null)
                throw new AdNotFoundException("Ad not found");

            if (request.UserId != ad.OwnerId)
                throw new UserWithAdOwnerDoesntEqualsException("User not equals owner ad");

            ad.Name = request.UpdatingAdDto.Name;
            ad.AdType = AdType.FromValue<AdType>(request.UpdatingAdDto.TypeId);
            ad.Comment = request.UpdatingAdDto.Comment;

            var updatedAd = _adRepository.Update(ad);
            await _adRepository.UnitOfWork.SaveChangesAsync();

            return new AdViewModel
            {
                Name = updatedAd.Name,
                Id = updatedAd.Id.Value,
                Comment = updatedAd.Comment,
                OwnerId = updatedAd.OwnerId,
                AdType = new AdTypeViewModel
                {
                    Id = updatedAd.AdType.Id,
                    Name = updatedAd.AdType.Name
                }
            };
        }
    }
}
