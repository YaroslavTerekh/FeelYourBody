using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Realizations;

public class FileService : IFileService
{
    private readonly DataContext _context;
    private readonly IHostEnvironment _env;

    public FileService
    (
        DataContext context,
        IHostEnvironment env
    )
    {
        _context = context;
        _env = env;
    }

    public async Task<AppFile> UploadFileAsync(AppFile fileModel, IFormFile file, CancellationToken cancellationToken)
    {
        var fileName = Path.GetFileName(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        var filePathName = fileName + DateTime.UtcNow.Millisecond + extension;
        var path = Path.Combine("uploads", filePathName);
        var uploadPath = Path.Combine(_env.ContentRootPath, "uploads", filePathName);

        try
        {
            fileModel.FilePath = path;
            fileModel.FileExtension = extension;
            fileModel.FileName = fileName;

            //Directory.CreateDirectory(uploadPath);
            using(var fs = new FileStream(uploadPath, FileMode.CreateNew))
            {
                await file.CopyToAsync(fs, cancellationToken);
            }

            await _context.Files.AddAsync(fileModel, cancellationToken);

            return fileModel;
        }
        catch(Exception e)
        {
            File.Delete(uploadPath);
            throw;
        }
    }

    public async Task DeleteFileAsync(Guid id, CancellationToken cancellationToken)
    {
        var file = await _context.Files.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if(file is null)
        {
            throw new NotFoundException(ErrorMessages.FileNotFound);
        }

        var path = Path.Combine(_env.ContentRootPath, file.FilePath);

        _context.Files.Remove(file);
        await _context.SaveChangesAsync(cancellationToken);

        File.Delete(path);
    }

    public async Task DeleteFileListAsync(List<Guid> ids, bool saveChanges, CancellationToken cancellationToken)
    {
        var files = await _context.Files.Where(t => ids.Contains(t.Id)).ToListAsync(cancellationToken);

        _context.Files.RemoveRange(files);
        if(saveChanges) await _context.SaveChangesAsync(cancellationToken);

        foreach(var file in files)
        {
            var path = Path.Combine(_env.ContentRootPath, file.FilePath);
            File.Delete(path);
        }   
    }
}
