using FluentValidation;
using Identity.API.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Application.Validations
{
    public class AuthenticateDtoValidator : AbstractValidator<AuthenticateModel>
    {
        public AuthenticateDtoValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email");

            RuleFor(m => m.Password)
                .Password();
        }
    }
}
