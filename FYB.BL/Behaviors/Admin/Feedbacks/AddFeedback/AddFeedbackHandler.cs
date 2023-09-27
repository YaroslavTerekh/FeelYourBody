using FYB.BL.Services.Abstractions;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.AddFeedback;

public class AddFeedbackHandler : IRequestHandler<AddFeedbackCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public AddFeedbackHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(AddFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = new Feedback
        {
            FeedbackText = request.FeedbackText,
            InstagramLink = request.InstagramLink,
            CoachingId = request.CoachingId,
        };

        foreach (var file in request.Photos)
        {
            var result = await _fileService.UploadFileAsync(new AppFile { FeedBackId = feedback.Id }, file, cancellationToken, null);

            feedback.Photos.Add(result);
        }

        await _context.Feedbacks.AddAsync(feedback, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
