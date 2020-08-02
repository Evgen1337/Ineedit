using Ads.API.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Validations
{
    public class UpdatingAdViewModelValidator : AbstractValidator<UpdatingAdViewModel>
    {
        public UpdatingAdViewModelValidator()
        {
            RuleFor(m => m.AdId)
                .NotEmpty()
                .WithMessage("Ad id is required");

            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(200)
                .MinimumLength(10)
                .WithMessage("Min length 10 and max length 200");

            RuleFor(m => m.Comment)
               .MaximumLength(5000)
               .WithMessage("Max length 5000");

            RuleFor(m => m.TypeId)
                .NotEmpty()
                .WithMessage("Type id is required");
        }
    }
}
