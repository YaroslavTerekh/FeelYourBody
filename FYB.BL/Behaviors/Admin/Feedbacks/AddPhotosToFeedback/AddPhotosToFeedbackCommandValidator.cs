using FluentValidation;
using FYB.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.AddPhotosToFeedback;

public class AddPhotosToFeedbackCommandValidator : AbstractValidator<AddPhotosToFeedbackCommand>
{
    public AddPhotosToFeedbackCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);
    }
}
