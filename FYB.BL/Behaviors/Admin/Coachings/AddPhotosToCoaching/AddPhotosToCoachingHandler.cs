using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.AddPhotosToCoaching;

public class AddPhotosToCoachingHandler : IRequestHandler<AddPhotosToCoachingCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public AddPhotosToCoachingHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(AddPhotosToCoachingCommand request, CancellationToken cancellationToken)
    {
        var coaching = await _context.Coachings
            .Include(t => t.ExamplePhotos)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if(coaching is null)
        {
            throw new NotFoundException(ErrorMessages.CoachingNotFound);
        }

        coaching.ExamplePhotos.Add(await _fileService.UploadFileAsync(new AppFile { CoachingListId = coaching.Id, OrderId = request.OrderId }, request.PhotoFile, cancellationToken));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
