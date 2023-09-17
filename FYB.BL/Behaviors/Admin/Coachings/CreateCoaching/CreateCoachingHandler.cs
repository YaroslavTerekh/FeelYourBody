using FYB.BL.Services.Abstractions;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.CreateCoaching;

internal class CreateCoachingHandler : IRequestHandler<CreateCoachingCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;
    private readonly IUnixService _unixService;

    public CreateCoachingHandler(DataContext context, IFileService fileService, IUnixService unixService)
    {
        _context = context;
        _fileService = fileService;
        _unixService = unixService;
    }

    public async Task<Unit> Handle(CreateCoachingCommand request, CancellationToken cancellationToken)
    {
        var coaching = new Coaching
        {
            Title = request.Title,
            Description = request.Description,
            CoachId = request.CoachId,
            Price = request.Price,
            UnixExpireTime = request.AccessDays
        };

        coaching.CoachingPhoto = await _fileService.UploadFileAsync(new AppFile { CoachingId = coaching.Id, OrderId = 1}, request.CoachingPhoto, cancellationToken);
        await _context.Coachings.AddAsync(coaching, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
