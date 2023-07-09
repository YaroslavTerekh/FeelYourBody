using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.DeleteFAQ;

public class DeleteFAQCommandValidator : AbstractValidator<DeleteFAQCommand>
{
    public DeleteFAQCommandValidator()
    {
        RuleFor(t => t.FAQId)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);
    }
}
