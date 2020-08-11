using Ads.Dtos.Ad;
using FluentValidation;

namespace Ads.API.Application.Validations
{
    public class UpdatingAdDtoModelValidator : AbstractValidator<UpdatingAdDto>
    {
        public UpdatingAdDtoModelValidator()
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
