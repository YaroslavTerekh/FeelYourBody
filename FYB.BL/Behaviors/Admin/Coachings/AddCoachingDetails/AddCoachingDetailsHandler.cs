using FYB.BL.Exceptions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.AddCoachingDetails;

public class AddCoachingDetailsHandler : IRequestHandler<AddCoachingDetailsCommand>
{
    private readonly DataContext _context;

    public AddCoachingDetailsHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddCoachingDetailsCommand request, CancellationToken cancellationToken)
    {
        var coaching = await _context.Coachings
            .FirstOrDefaultAsync(t => t.Id == request.Id);

        if (coaching is null) throw new NotFoundException(ErrorMessages.CoachingNotFound);

        foreach (var detail in request.CoachingDetails)
        {
            var entity = new CoachingDetails
            {
                Icon = detail.Icon,
                Detail = detail.Detail,
                CoachingId = coaching.Id
            };

            await _context.CoachingDetails.AddAsync(entity, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
