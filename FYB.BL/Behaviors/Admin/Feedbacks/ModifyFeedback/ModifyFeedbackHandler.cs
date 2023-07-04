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

namespace FYB.BL.Behaviors.Admin.Feedbacks.ModifyFeedback;

public class ModifyFeedbackHandler : IRequestHandler<ModifyFeedbackCommand>
{
    private readonly DataContext _context;

    public ModifyFeedbackHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ModifyFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await _context.Feedbacks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (feedback is null) throw new NotFoundException(ErrorMessages.FeedbackNotFound);

        feedback.FeedbackText = request.FeedbackText;
        feedback.InstagramLink = request.InstagramLink;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
