using Ads.API.Application.ViewModels;
using Ads.Dtos.Ad;
using MediatR;
using System;

namespace Ads.API.Application.Commands
{
    public class CreateAdCommand : IRequest<AdViewModel>
    {
        public CreateAdCommand(Guid userId, CreatingAdDto creatingAdDto)
        {
            UserId = userId;
            CreatingAdDto = creatingAdDto;
        }

        public Guid UserId { get; }

        public CreatingAdDto CreatingAdDto { get; }
    }
}
