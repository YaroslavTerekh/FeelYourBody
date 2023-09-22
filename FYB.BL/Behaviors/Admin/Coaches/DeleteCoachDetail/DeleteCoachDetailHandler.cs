using FYB.BL.Exceptions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.DeleteCoachDetail;

public class DeleteCoachDetailHandler : IRequestHandler<DeleteCoachDetailCommand>
{
    public readonly DataContext _context;

    public DeleteCoachDetailHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCoachDetailCommand request, CancellationToken cancellationToken)
    {
        var detail = await _context.CoachDetails.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (detail is null)
            throw new NotFoundException(ErrorMessages.CoachDetailNotFound);

        _context.CoachDetails.Remove(detail);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
