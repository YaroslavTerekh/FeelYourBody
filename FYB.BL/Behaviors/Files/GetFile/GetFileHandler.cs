using FYB.BL.Exceptions;
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

namespace FYB.BL.Behaviors.Files.GetFile;

public class GetFileHandler : IRequestHandler<GetFileQuery, AppFile>
{
    private readonly DataContext _context;

    public GetFileHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<AppFile> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var file = await _context.Files.FirstOrDefaultAsync(t => t.Id == request.Id);

        if (file is null) throw new NotFoundException(ErrorMessages.FileNotFound);

        return file;
    }
}
