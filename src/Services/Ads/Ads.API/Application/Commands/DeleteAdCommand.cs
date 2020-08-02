using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Commands
{
    public class DeleteAdCommand : IRequest<Unit>
    {
        public DeleteAdCommand(Guid userId, int adId)
        {
            UserId = userId;
            AdId = adId;
        }

        public Guid UserId { get; }
        public int AdId { get; }
    }
}
