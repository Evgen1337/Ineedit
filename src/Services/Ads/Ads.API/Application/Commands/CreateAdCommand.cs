using Ads.API.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Commands
{
    public class CreateAdCommand : IRequest<AdViewModel>
    {
        public CreateAdCommand(Guid userId, CreatingAdViewModel ad)
        {
            UserId = userId;
            Ad = ad;
        }

        public Guid UserId { get; }

        public CreatingAdViewModel Ad { get; }
    }
}
