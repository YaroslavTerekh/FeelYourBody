using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Realizations;

public class VideoService : IVideoService
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _env;

    public VideoService(DataContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task UploadVideoAsync(IFormFile file, Guid coachingId, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(file.FileName);

        if (extension == ".mp4")
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePathName = fileName + "_" + Guid.NewGuid() + extension;
            var path = Path.Combine("uploads", "videos", filePathName);
            var uploadPath = Path.Combine(_env.ContentRootPath, "uploads", "videos", filePathName);

            try
            {
                var coaching = await _context.Coachings.FirstOrDefaultAsync(t => t.Id == coachingId, cancellationToken);
                if (coaching is null) throw new NotFoundException(ErrorMessages.CoachingNotFound);

                var fileModel = new CoachingVideo
                {
                    CoachingId = coachingId,
                    ContentFileType = file.ContentType,
                    FileName = fileName,
                    Path = path
                };

                using (var fs = new FileStream(uploadPath, FileMode.CreateNew))
                {
                    await file.CopyToAsync(fs, cancellationToken);
                }

                await _context.CoachingVideos.AddAsync(fileModel, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                File.Delete(uploadPath);
                throw;
            }
        } else
        {
            throw new Exception(ErrorMessages.UnknownVideoType);
        }
    }

    public async Task DeleteVideoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _context.CoachingVideos.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (file is null)
        {
            throw new NotFoundException(ErrorMessages.FileNotFound);
        }

        var path = Path.Combine(_env.ContentRootPath, file.Path);

        _context.CoachingVideos.Remove(file);
        await _context.SaveChangesAsync(cancellationToken);

        File.Delete(path);
    }
}
