using Ads.Dtos.Ad;
using FluentValidation;

namespace Ads.API.Application.Validations
{
    public class GettingAdDtoModelValidator : AbstractValidator<GettingAdDto>
    {
        public GettingAdDtoModelValidator()
        {
            RuleFor(m => m.AdId)
                .NotEmpty()
                .WithMessage("Ad id is required");
        }
    }
}
