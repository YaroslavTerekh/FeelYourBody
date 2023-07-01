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
        var coach = await _context.Coaches.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (coach is null)
        {
            throw new NotFoundException(ErrorMessages.CoachNotFound);
        }

        _context.Coaches.Remove(coach);
        await _context.SaveChangesAsync(cancellationToken);

        var filesToDelete = new List<Guid>() { coach.AvatarId };
        filesToDelete.AddRange(coach.Coachings.Select(t => t.CoachingPhotoId).ToList());
        filesToDelete.AddRange(coach.Coachings.SelectMany(t => t.ExamplePhotos.Select(ep => ep.Id)));

        await _fileService.DeleteFileListAsync(filesToDelete, cancellationToken);

        return Unit.Value;
    }
}
