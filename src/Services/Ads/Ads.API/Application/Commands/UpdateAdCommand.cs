using Ads.API.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Commands
{
    public class UpdateAdCommand : IRequest<AdViewModel>
    {
        public UpdateAdCommand(UpdatingAdViewModel adViewModel, Guid userId)
        {
            Ad = adViewModel;
            UserId = userId;
        }

        public UpdatingAdViewModel Ad { get; }

        public Guid UserId { get; }
    }
}
