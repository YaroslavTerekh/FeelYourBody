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

namespace FYB.BL.Behaviors.Admin.Coaches.AddPhotoToCoach;

public class AddPhotoToCoachHandler : IRequestHandler<AddPhotoToCoachCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public AddPhotoToCoachHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(AddPhotoToCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = await _context.Coaches
            .Include(t => t.Photos)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (coach is null)
            throw new NotFoundException(ErrorMessages.CoachNotFound);

        coach.Photos.ForEach(t => t.CoachId = null);
        foreach (var photo in request.Files)
        {
            var resPhoto = await _fileService.UploadFileAsync(new AppFile { CoachId = request.Id, FileName = request.FileName }, photo, cancellationToken);
            await _context.Files.AddAsync(resPhoto, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
