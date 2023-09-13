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

namespace FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetailsParent;

public class DeleteCoachingDetailsParentHandler : IRequestHandler<DeleteCoachingDetailsParentCommand>
{
    private readonly DataContext _context;

    public DeleteCoachingDetailsParentHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCoachingDetailsParentCommand request, CancellationToken cancellationToken)
    {
        var detailsParent = await _context.CoachingDetailParents
            .FirstOrDefaultAsync(t => t.Id == request.ParentId, cancellationToken);

        if (detailsParent is null)
            throw new NotFoundException(ErrorMessages.CoachingDetailParentNotFound);

        _context.CoachingDetailParents.Remove(detailsParent);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
