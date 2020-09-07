using Ads.API.Application.ViewModels;
using Ads.Domain.AggregatesModel.AdAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ads.API.Application.Commands
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, AdViewModel>
    {
        private readonly IAdRepository _adRepository;

        public CreateAdCommandHandler(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public async Task<AdViewModel> Handle(CreateAdCommand command, CancellationToken cancellationToken)
        {
            var adToInsert = new Ad(
                command.UserId,
                command.CreatingAdDto.Name,
                AdType.FromValue<AdType>(command.CreatingAdDto.TypeId),
                command.CreatingAdDto.Comment
            );

            var ad = await _adRepository.AddAsync(adToInsert);
            await _adRepository.UnitOfWork.SaveChangesAsync();

            return new AdViewModel
            {
                Name = ad.Name,
                Id = ad.Id.Value,
                Comment = ad.Comment,
                OwnerId = ad.OwnerId,
                AdType = new AdTypeViewModel
                {
                    Id = ad.AdType.Id,
                    Name = ad.AdType.Name
                }
            };
        }
    }
}
