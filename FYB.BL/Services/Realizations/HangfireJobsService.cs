using FYB.BL.Services.Abstractions;
using FYB.Data.DbConnection;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Realizations;

public class HangfireJobsService : IHangfireJobsService
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HangfireJobsService(
        DataContext context, 
        IFileService fileService, 
        IWebHostEnvironment webHostEnvironment
    )
    {
        _context = context;
        _fileService = fileService;
        _webHostEnvironment = webHostEnvironment;
    }

    public void CreateFileDeletingJob()
    {
        RecurringJob.AddOrUpdate("CheckEmptyFiles", () => DeleteAllEmptyFiles(), Cron.Daily);
    }

    public void CreateInvisibleFilesDeletingJob()
    {
        RecurringJob.AddOrUpdate("CheckInvisibleFiles", () => DeleteInvisibleFiles(), Cron.Weekly);
    }

    public async Task DeleteAllEmptyFiles()
    {
        var emptyFiles = await _context.Files
            .Where(t => t.CoachId == null &&
                        t.CoachingId == null &&
                        t.CoachingListId == null &&
                        t.FeedBackId == null
                  )
            .Select(t => t.Id)
            .ToListAsync();

        await _fileService.DeleteFileListAsync(emptyFiles, true);
    }

    public async Task DeleteInvisibleFiles()
    {
        var allFiles = Directory.GetFiles(Path.Combine(_webHostEnvironment.ContentRootPath, "uploads")).ToList();
        var allDbFiles = await _context.Files.Select(t => Path.Combine(_webHostEnvironment.ContentRootPath, t.FilePath)).ToListAsync();

        var invisibleFiles = allFiles.Except(allDbFiles).ToList();

        foreach (var file in invisibleFiles)
        {
            File.Delete(file);
        }
    }
}
