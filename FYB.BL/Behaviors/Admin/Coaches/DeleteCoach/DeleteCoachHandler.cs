using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.DeleteCoach;

public class DeleteCoachHandler : IRequestHandler<DeleteCoachCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public DeleteCoachHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = await _context.Coaches
            .Include(t => t.Photos)
            .Include(t => t.Coachings)
                .ThenInclude(t => t.Feedbacks)
                .ThenInclude(t => t.Photos)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (coach is null)
        {
            throw new NotFoundException(ErrorMessages.CoachNotFound);
        }

        coach.Photos.ForEach(t => t.CoachId = null);
        foreach (var coaching in coach.Coachings)
            foreach (var feedback in coaching.Feedbacks)
                foreach (var photo in feedback.Photos)
                    photo.FeedBackId = null;
        _context.Coaches.Remove(coach);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
