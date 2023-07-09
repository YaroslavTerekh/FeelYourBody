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

namespace FYB.BL.Behaviors.Admin.Feedbacks.DeleteFeedback;

public class DeleteFeedbackHandler : IRequestHandler<DeleteFeedbackCommand>
{
    private readonly DataContext _context;

    public DeleteFeedbackHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await _context.Feedbacks
            .Include(t => t.Photos)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (feedback is null) throw new NotFoundException(ErrorMessages.FeedbackNotFound);

        foreach(var file in feedback.Photos) file.FeedBackId = null;
        _context.Feedbacks.Remove(feedback);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
