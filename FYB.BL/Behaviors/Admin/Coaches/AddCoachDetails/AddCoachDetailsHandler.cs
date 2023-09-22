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

namespace FYB.BL.Behaviors.Admin.Coaches.AddCoachDetails;

public class AddCoachDetailsHandler : IRequestHandler<AddCoachDetailsCommand>
{
    private readonly DataContext _context;

    public AddCoachDetailsHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddCoachDetailsCommand request, CancellationToken cancellationToken)
    {
        var coach = await _context.Coaches
            .Include(t => t.CoachDetails)
            .FirstOrDefaultAsync(t => t.Id == request.CoachId, cancellationToken);

        if (coach is null)
            throw new NotFoundException(ErrorMessages.CoachNotFound);

        var detail = new CoachDetails
        {
            Detail = request.Detail,
            CoachId = coach.Id
        };

        await _context.CoachDetails.AddAsync(detail, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
