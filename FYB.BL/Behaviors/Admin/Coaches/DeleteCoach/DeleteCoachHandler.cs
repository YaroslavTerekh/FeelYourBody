using FYB.BL.Services.Abstractions;
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

        _context.Coaches.Remove(coach);
        await _context.SaveChangesAsync(cancellationToken);

        await _fileService.DeleteFileAsync(coach.AvatarId, cancellationToken);

        return Unit.Value;
    }
}
