using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.AddPhotosToFeedback;

public class AddPhotosToFeedbackHandler : IRequestHandler<AddPhotosToFeedbackCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public AddPhotosToFeedbackHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(AddPhotosToFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await _context.Feedbacks
            .Include(t => t.Photos)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (feedback is null) throw new NotFoundException(ErrorMessages.FeedbackNotFound);

        foreach(var file in request.Photos)
        {
            var result = await _fileService.UploadFileAsync(new AppFile { FeedBackId = feedback.Id }, file, cancellationToken, null);

            feedback.Photos.Add(result);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
