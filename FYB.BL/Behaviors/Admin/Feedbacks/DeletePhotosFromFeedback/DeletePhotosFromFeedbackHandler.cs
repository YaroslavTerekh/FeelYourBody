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

namespace FYB.BL.Behaviors.Admin.Feedbacks.DeletePhotosFromFeedback;

public class DeletePhotosFromFeedbackHandler : IRequestHandler<DeletePhotosFromFeedbackCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public DeletePhotosFromFeedbackHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeletePhotosFromFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await _context.Feedbacks
            .Include(t => t.Photos)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (feedback is null) throw new NotFoundException(ErrorMessages.FeedbackNotFound);

        var photosToDelete = feedback.Photos.Where(t => request.PhotoIds.Contains(t.Id)).ToList().Select(t => t.Id).ToList();

        await _fileService.DeleteFileListAsync(photosToDelete, true, cancellationToken);

        return Unit.Value;
    }
}
