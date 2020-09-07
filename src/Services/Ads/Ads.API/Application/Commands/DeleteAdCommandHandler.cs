using Ads.API.Application.Exceptions;
using Ads.Domain.AggregatesModel.AdAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ads.API.Application.Commands
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, Unit>
    {
        private readonly IAdRepository _adRepository;

        public DeleteAdCommandHandler(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public async Task<Unit> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var ad = await _adRepository.GetByIdAsync(request.AdId);

            if (ad is null)
                throw new AdNotFoundException("Ad not found");

            var adOwnerId = ad.OwnerId;

            if (adOwnerId != request.UserId)
                throw new UserWithAdOwnerDoesntEqualsException("User not equals owner ad");

            await _adRepository.DeleteAsync(request.AdId);
            await _adRepository.UnitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
