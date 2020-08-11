using Ads.Dtos.Ad;
using FluentValidation;

namespace Ads.API.Application.Validations
{
    public class DeletingAdDtoModelValidator : AbstractValidator<DeletingAdDto>
    {
        public DeletingAdDtoModelValidator()
        {
            RuleFor(m => m.AdId)
                .NotEmpty()
                .WithMessage("Ad id is required");
        }
    }
}
