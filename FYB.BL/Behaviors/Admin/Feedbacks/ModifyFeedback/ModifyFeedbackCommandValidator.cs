using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.ModifyFeedback;

public class ModifyFeedbackCommandValidator : AbstractValidator<ModifyFeedbackCommand>
{
    public ModifyFeedbackCommandValidator()
    {
        RuleFor(t => t.FeedbackText)
            .MinimumLength(5)
            .WithMessage(ValidationMessages.DescriptionTooShort)
            .MaximumLength(100)
            .WithMessage(ValidationMessages.DescriptionTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.DescriptionRequired);

        RuleFor(t => t.InstagramLink)
            .NotEmpty()
            .WithMessage(ValidationMessages.InstagramLinkRequired);

        RuleFor(t => t.Id)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);
    }
}
