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

namespace FYB.BL.Behaviors.Admin.Coaches.ModifyCoachDetail;

public class ModifyCoachDetailHandler : IRequestHandler<ModifyCoachDetailCommand>
{
    private readonly DataContext _context;

    public ModifyCoachDetailHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ModifyCoachDetailCommand request, CancellationToken cancellationToken)
    {
        var detail = await _context.CoachDetails.FirstOrDefaultAsync(t => t.Id == request.DetailId, cancellationToken);

        if (detail is null)
            throw new NotFoundException(ErrorMessages.CoachDetailNotFound);

        detail.Detail = request.Detail;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
