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

namespace FYB.BL.Behaviors.Admin.Coachings.DeletePhotosFromCoaching;

public class DeletePhotosFromCoachingHandler : IRequestHandler<DeletePhotosFromCoachingCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public DeletePhotosFromCoachingHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeletePhotosFromCoachingCommand request, CancellationToken cancellationToken)
    {
        var coaching = await _context.Coachings
            .Include(t => t.ExamplePhotos)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (coaching is null) throw new NotFoundException(ErrorMessages.CoachingNotFound);

        var photosToDelete = new List<Guid>();
        var examplesIds = coaching.ExamplePhotos.Select(t => t.Id).ToList();

        foreach (var id in request.PhotoIds)
        {
            if(examplesIds.Contains(id))  photosToDelete.Add(id);
        }

        await _fileService.DeleteFileListAsync(photosToDelete, true, cancellationToken);

        return Unit.Value;
    }
}
