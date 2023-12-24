using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;

public class ModifyCoachCommandValidator : AbstractValidator<ModifyCoachCommand>
{
    public ModifyCoachCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);

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

        //RuleFor(t => t.Description)
        //    .MinimumLength(50)
        //    .WithMessage(ValidationMessages.DescriptionTooShort)
        //    .MaximumLength(500)
        //    .WithMessage(ValidationMessages.DescriptionTooLong)
        //    .NotEmpty()
        //    .WithMessage(ValidationMessages.DescriptionRequired);

        RuleFor(t => t.InstagramLink)
            .NotEmpty()
            .WithMessage(ValidationMessages.InstagramLinkRequired);

        RuleFor(t => t.BirthDate)
            .LessThan(DateTime.UtcNow)
            .WithMessage(ValidationMessages.WrongDateTime);
    }
}
