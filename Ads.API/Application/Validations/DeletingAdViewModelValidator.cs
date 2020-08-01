using Ads.API.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Validations
{
    public class DeletingAdViewModelValidator : AbstractValidator<DeletingAdViewModel>
    {
        public DeletingAdViewModelValidator()
        {
            RuleFor(m => m.AdId)
                .NotEmpty()
                .WithMessage("Ad id is required");
        }
    }
}
