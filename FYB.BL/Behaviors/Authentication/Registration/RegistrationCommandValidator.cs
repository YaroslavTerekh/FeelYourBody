using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.Registration;

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(t => t.FirstName)
            .MinimumLength(2)
            .WithMessage(ValidationMessages.FirstNameTooShort)
            .MaximumLength(20)
            .WithMessage(ValidationMessages.FirstNameTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.FirstNameRequired);

        RuleFor(t => t.LastName)
            .MinimumLength(2)
            .WithMessage(ValidationMessages.LastNameTooShort)
            .MaximumLength(20)
            .WithMessage(ValidationMessages.LastNameTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.LastNameRequired);

        RuleFor(t => t.Email)
            .EmailAddress()
            .WithMessage(ValidationMessages.WrongEmail)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmailRequired);
    }
}
