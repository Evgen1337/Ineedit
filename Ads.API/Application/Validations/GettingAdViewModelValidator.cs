using Ads.API.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Validations
{
    public class GettingAdViewModelValidator : AbstractValidator<GettingAdViewModel>
    {
        public GettingAdViewModelValidator()
        {
            RuleFor(m => m.AdId)
                .NotEmpty()
                .WithMessage("Ad id is required");
        }
    }
}
