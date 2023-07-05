using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.CreateCoaching;

public class CreateCoachingCommandValidator : AbstractValidator<CreateCoachingCommand>
{
    public CreateCoachingCommandValidator()
    {
        RuleFor(t => t.Title)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TitleTooShort)
            .MaximumLength(40)
            .WithMessage(ValidationMessages.TitleTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.TitleRequired);

        RuleFor(t => t.Description)
            .MinimumLength(100)
            .WithMessage(ValidationMessages.DescriptionTooShort)
            .MaximumLength(1000)
            .WithMessage(ValidationMessages.DescriptionTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.DescriptionRequired);

        RuleFor(t => t.Price)
            .GreaterThan(0)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.WrongPrice);

        RuleFor(t => t.CoachId)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);
    }
}
