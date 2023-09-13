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

namespace FYB.BL.Behaviors.Admin.Coachings.ChangeDetailsParentTitle;

public class ChangeDetailsParentTitleHandler : IRequestHandler<ChangeDetailsParentTitleCommand>
{
    private readonly DataContext _context;

    public ChangeDetailsParentTitleHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ChangeDetailsParentTitleCommand request, CancellationToken cancellationToken)
    {
        var detailsParent = await _context.CoachingDetailParents
            .FirstOrDefaultAsync(t => t.Id == request.ParentId, cancellationToken);

        if (detailsParent is null)
            throw new NotFoundException(ErrorMessages.CoachingDetailParentNotFound);

        detailsParent.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
