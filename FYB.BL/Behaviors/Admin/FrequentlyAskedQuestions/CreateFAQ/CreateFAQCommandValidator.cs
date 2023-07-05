using FluentValidation;
using FYB.Data.Constants;
using FYB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.CreateFAQ;

public class CreateFAQCommandValidator : AbstractValidator<CreateFAQCommand>
{
    public CreateFAQCommandValidator()
    {
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
