using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetails;

public class DeleteCoachingDetailsHandler : IRequestHandler<DeleteCoachingDetailsCommand>
{
    private readonly DataContext _context;

    public DeleteCoachingDetailsHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCoachingDetailsCommand request, CancellationToken cancellationToken)
    {
        var details = await _context.CoachingDetails.Where(t => request.Ids.Contains(t.Id)).ToListAsync(cancellationToken);

        _context.CoachingDetails.RemoveRange(details);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
