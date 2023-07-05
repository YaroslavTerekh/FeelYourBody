using FluentValidation;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace FYB.BL.Behaviors.Admin.Coachings.DeletePhotosFromCoaching;

public class DeletePhotosFromCoachingCommandValidator : AbstractValidator<DeletePhotosFromCoachingCommand>
{
    public DeletePhotosFromCoachingCommandValidator(DataContext _context)
    {
        RuleFor(t => t.Id)
            .MustAsync(async (id, cancellationToken)
                        => await _context.Coachings.Where(t => t.Id == id).AnyAsync(cancellationToken))
            .WithMessage(ValidationMessages.IdWrong)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);
    }
}
