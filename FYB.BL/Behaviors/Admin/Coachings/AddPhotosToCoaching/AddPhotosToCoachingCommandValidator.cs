using FluentValidation;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.AddPhotosToCoaching;

public class AddPhotosToCoachingCommandValidator : AbstractValidator<AddPhotosToCoachingCommand>
{
    public AddPhotosToCoachingCommandValidator(DataContext _context)
    {
        RuleFor(t => t.Id)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdWrong)
            .NotEmpty()
            .WithMessage(ValidationMessages.IdRequired);
    }
}
