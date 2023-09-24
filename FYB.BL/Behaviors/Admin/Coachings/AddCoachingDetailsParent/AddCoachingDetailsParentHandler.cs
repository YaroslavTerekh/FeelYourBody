using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.AddCoachingDetailsParent;

public class AddCoachingDetailsParentHandler : IRequestHandler<AddCoachingDetailsParentCommand, Guid>
{
    private readonly DataContext _context;

    public AddCoachingDetailsParentHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddCoachingDetailsParentCommand request, CancellationToken cancellationToken)
    {
        var parent = new CoachingDetailsParent
        {
            CoachingId = request.CoachingId,
            Title = request.Title
        };

        await _context.CoachingDetailParents.AddAsync(parent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return parent.Id;
    }
}
