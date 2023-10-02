using FYB.BL.Behaviors.Admin.Coachings.AddPhotosToCoaching;
using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.AddPhotosToFood;

public class AddPhotosToFoodHandler : IRequestHandler<AddPhotosToFoodCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;
    private readonly IHostEnvironment _env;

    public AddPhotosToFoodHandler(DataContext context, IFileService fileService, IHostEnvironment configuration)
    {
        _context = context;
        _fileService = fileService;
        _env = configuration;
    }

    public async Task<Unit> Handle(AddPhotosToFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _context.Food
            .Include(t => t.Photos)
            .FirstOrDefaultAsync(t => t.Id == request.FoodId, cancellationToken);

        if (food is null)
            throw new NotFoundException(ErrorMessages.FoodNotFound);

        await UploadAsync(request.File, food.Id, request.OrderId, request.FileName, cancellationToken);
        return Unit.Value;
    }

    private async Task UploadAsync(IFormFile file, Guid foodId, int? orderId, string fileName, CancellationToken cancellationToken)
    {
        var fullFileName = Path.GetFileName(fileName ?? file.FileName);
        var filePathName = Guid.NewGuid() + Path.GetExtension(fileName ?? file.FileName);
        var path = Path.Combine("uploads", filePathName);
        var uploadPath = Path.Combine(_env.ContentRootPath, "uploads", filePathName);

        try
        {
            var fileModel = new FoodPhoto();

            fileModel.FilePath = path;
            fileModel.FileExtension = file.ContentType;
            fileModel.FileName = fullFileName;
            fileModel.OrderId = orderId;
            fileModel.FoodId = foodId;

            using (var fs = new FileStream(uploadPath, FileMode.CreateNew))
            {
                await file.CopyToAsync(fs, cancellationToken);
            }

            await _context.FoodPhotos.AddAsync(fileModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }
        catch (Exception e)
        {
            File.Delete(uploadPath);
            throw;
        }
    }
}
