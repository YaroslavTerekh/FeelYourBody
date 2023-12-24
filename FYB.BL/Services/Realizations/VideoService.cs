using Firebase.Auth;
using Firebase.Storage;
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
    private static string ApiKey = "AIzaSyC0cG4x8WTL1X-2uiLIcp-IYBT2u7zO-_E";
    private static string Bucket = "feelyourbody-b444f.appspot.com";
    private static string AuthEmail = "feelbodycom@body.com";
    private static string AuthPassword = "Pa$$word313!";

    public VideoService(DataContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task UploadVideoAsync(IFormFile file, Guid coachingId, bool isPreview = false, string? name = null, CancellationToken cancellationToken = default)
    {

        var extension = Path.GetExtension(name ?? file.FileName);

        var fileName = Path.GetFileName(name ?? file.FileName);
        var filePathName = fileName + "_" + Guid.NewGuid() + extension;
        var path = Path.Combine("uploads", filePathName);
        var uploadPath = Path.Combine(_env.ContentRootPath, "uploads", filePathName);

        var stream = file.OpenReadStream();

        var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

        var cancellation = new CancellationTokenSource();

        var task = new FirebaseStorage(
            Bucket,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            })
            .Child("videos")
            .Child(fileName)
            .PutAsync(stream, cancellation.Token);

        task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

        try
        {
            var videoLink = await task;

            var coaching = await _context.Coachings
                    .Include(t => t.Videos)
                    .FirstOrDefaultAsync(t => t.Id == coachingId, cancellationToken);

            if (coaching is null)
                throw new NotFoundException(ErrorMessages.CoachingNotFound);

            if (coaching.Videos.Where(t => t.IsPreview == true).ToList().Count > 0 && isPreview)
                throw new Exception(ErrorMessages.PreviewExists);

            var fileModel = new CoachingVideo
            {
                CoachingId = coachingId,
                ContentFileType = file.ContentType,
                IsPreview = isPreview,
                FileName = fileName,
                Path = videoLink
            };

            await _context.CoachingVideos.AddAsync(fileModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: {0}", ex);
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
