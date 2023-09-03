using FYB.BL.Services.Abstractions;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
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
    private readonly IUnixService _unixService;
    private readonly IFileService _fileService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HangfireJobsService(
        DataContext context, 
        IFileService fileService,
        IWebHostEnvironment webHostEnvironment,
        IUnixService unixService)
    {
        _context = context;
        _fileService = fileService;
        _webHostEnvironment = webHostEnvironment;
        _unixService = unixService;
    }

    public void CreateFileDeletingJob()
    {
        RecurringJob.AddOrUpdate("CheckEmptyFiles", () => DeleteAllEmptyFiles(), Cron.Daily);
    }

    public void CreateInvisibleFilesDeletingJob()
    {
        RecurringJob.AddOrUpdate("CheckInvisibleFiles", () => DeleteInvisibleFiles(), Cron.Weekly);
    }

    public void CreateJobForExpiringProduct(BaseProduct product, Guid currentUserId, string orderId)
    {
        BackgroundJob.Schedule(() => DeleteUserFromProductUsersList(product, currentUserId, orderId), DateTimeOffset.FromUnixTimeSeconds(_unixService.GenerateUnix(product.UnixExpireTime)));
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

    public async Task DeleteUserFromProductUsersList(BaseProduct product, Guid currentUserId, string orderId)
    {
        BaseProduct? productEntity = product.GetType() == typeof(Food) ?
                await _context.Food.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == product.Id) :
                await _context.Coachings.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == product.Id);
        var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == currentUserId);
        BasePurchase? purchase = product.GetType() == typeof(Purchase<Food>) ?
                await _context.FoodPurchases.FirstOrDefaultAsync(t => t.OrderId == orderId) :
                await _context.CoachingPurchases.FirstOrDefaultAsync(t => t.OrderId == orderId);

        if (user is not null)
        {
            productEntity.Users.Remove(user);
            purchase.IsExpired = true;
        }

        await _context.SaveChangesAsync();
    }
}
