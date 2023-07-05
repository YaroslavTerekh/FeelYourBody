using FluentValidation;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.AddFeedback;

public class AddFeedbackCommandValidator : AbstractValidator<AddFeedbackCommand>
{
    public AddFeedbackCommandValidator(DataContext context)
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

        RuleFor(t => t.CoachingId)
            .MustAsync(async (id, cancellationToken)
                        => await context.Coachings.FirstOrDefaultAsync(t => t.Id == id, cancellationToken) is not null)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);
    }
}
