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

namespace FYB.BL.Behaviors.Admin.Coaches.AddNewCoach;

public class AddNewCoachHandler : IRequestHandler<AddNewCoachCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public AddNewCoachHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(AddNewCoachCommand request, CancellationToken cancellationToken)
    {
        var newCoach = new Coach
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            InstagramLink = request.InstagramLink,
            Description = "",
            Coachings = request.CoachingIds is null ? null :
                        await _context.Coachings.Where(t => request.CoachingIds.Contains(t.Id)).ToListAsync(cancellationToken)
        };

        // foreach (var photo in request.Photos)
        // {
        //     var resPhoto = await _fileService.UploadFileAsync(new AppFile { CoachId = newCoach.Id }, photo, cancellationToken);
        //     await _context.Files.AddAsync(resPhoto, cancellationToken);
        // }

        await _context.Coaches.AddAsync(newCoach, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
