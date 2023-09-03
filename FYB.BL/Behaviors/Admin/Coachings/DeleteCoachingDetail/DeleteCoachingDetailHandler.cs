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

namespace FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetail;

public class DeleteCoachingDetailHandler : IRequestHandler<DeleteCoachingDetailCommand>
{
    private readonly DataContext _context;

    public DeleteCoachingDetailHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCoachingDetailCommand request, CancellationToken cancellationToken)
    {
        var detail = await _context.CoachingDetails.FirstOrDefaultAsync(t => t.Id == request.DetailId, cancellationToken);

        if (detail is null)
            throw new NotFoundException(ErrorMessages.CoachingDetailNotFound);

        _context.CoachingDetails.Remove(detail);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
