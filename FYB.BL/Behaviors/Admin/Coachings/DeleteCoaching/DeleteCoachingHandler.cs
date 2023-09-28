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

namespace FYB.BL.Behaviors.Admin.Coachings.DeleteCoaching;

public class DeleteCoachingHandler : IRequestHandler<DeleteCoachingCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public DeleteCoachingHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteCoachingCommand request, CancellationToken cancellationToken)
    {
        var coaching = await _context.Coachings
            .Include(t => t.Feedbacks)
                .ThenInclude(t => t.Photos)
            .Include(t => t.ExamplePhotos)
            .Include(t => t.CoachingPhoto)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if(coaching is null)
        {
            throw new NotFoundException(ErrorMessages.CoachingNotFound);
        }

        foreach (var file in coaching.ExamplePhotos) file.CoachingListId = null;
        foreach (var feedback in coaching.Feedbacks)
            foreach (var file in feedback.Photos) 
                file.FeedBackId = null;

        coaching.CoachingPhoto.CoachingId = null;
        _context.Coachings.Remove(coaching);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
