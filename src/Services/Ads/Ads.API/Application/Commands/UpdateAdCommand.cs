using Ads.API.Application.ViewModels;
using Ads.Dtos;
using Ads.Dtos.Ad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Commands
{
    public class UpdateAdCommand : IRequest<AdViewModel>
    {
        public UpdateAdCommand(UpdatingAdDto updatingAdDto, Guid userId)
        {
            UpdatingAdDto = updatingAdDto;
            UserId = userId;
        }

        public UpdatingAdDto UpdatingAdDto { get; }

        public Guid UserId { get; }
    }
}
