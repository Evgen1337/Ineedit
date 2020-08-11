using FluentValidation;
using Identity.API.Application.Dtos;
using Identity.API.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Application.Validations
{
    public class RegisterlDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterlDtoValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(m => m.Password)
                .Password();

            RuleFor(m => m.ConfirmPassword)
                .NotEmpty()
                .Equal(e => e.Password)
                .Password();

            RuleFor(m => m.PhoneNumber)
                .NotEmpty()
                .MatchPhoneNumberRule();

            RuleFor(m => m.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(40);
        }
    }
}
