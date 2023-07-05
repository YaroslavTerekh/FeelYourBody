using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.ModifyFAQ;

public class ModifyFAQCommandValidator : AbstractValidator<ModifyFAQCommand>
{
    public ModifyFAQCommandValidator()
    {
        RuleFor(t => t.FAQId)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);

        RuleFor(t => t.Question)
            .MinimumLength(5)
            .WithMessage(ValidationMessages.FAQTooShort)
            .MaximumLength(100)
            .WithMessage(ValidationMessages.FAQTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.FAQRequired);

        RuleFor(t => t.Answer)
            .MinimumLength(20)
            .WithMessage(ValidationMessages.FAQAnswerTooShort)
            .MaximumLength(300)
            .WithMessage(ValidationMessages.FAQAnswerTooLong)
            .NotEmpty()
            .WithMessage(ValidationMessages.FAQAnswerRequired);
    }
}
