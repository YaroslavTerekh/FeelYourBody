using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(t => t.Email)
            .EmailAddress()
            .WithMessage(ValidationMessages.WrongEmail)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmailRequired);
    }
}
