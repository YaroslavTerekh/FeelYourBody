//using FYB.BL.Exceptions;
//using FYB.Data.Constants;
//using FYB.Data.DbConnection;
//using FYB.Data.Entities;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FYB.BL.Behaviors.Admin.Foods.AddPhotoToFood;

//public class AddPhotoToFoodHandler : IRequestHandler<AddPhotoToFoodCommand>
//{
//    private readonly DataContext _context;
//    private readonly IHostEnvironment _env;

//    public AddPhotoToFoodHandler(DataContext context, IHostEnvironment hostEnvironment)
//    {
//        _context = context;
//        _env = hostEnvironment;
//    }

//    public async Task<Unit> Handle(AddPhotoToFoodCommand request, CancellationToken cancellationToken)
//    {
//        var food = await _context.Food
//            .Include(t => t.Avatar)
//            .Include(t => t.Photos)
//            .FirstOrDefaultAsync(t => t.Id == request.FoodId, cancellationToken);

//        if (food is null)
//            throw new NotFoundException(ErrorMessages.FoodNotFound);

//        if (food.Avatar is not null)
//        {
//            File.Delete(food.Avatar.FilePath);
//        }

//        food.AvatarId = await UploadAsync(request.File, request.FileName, cancellationToken);
//        await _context.SaveChangesAsync(cancellationToken);

//        return Unit.Value;
//    }

//    private async Task<Guid> UploadAsync(IFormFile file, string fileName, CancellationToken cancellationToken)
//    {
//        var fullFileName = Path.GetFileName(fileName ?? file.FileName);
//        var filePathName = Guid.NewGuid() + Path.GetExtension(fileName ?? file.FileName);
//        var path = Path.Combine("uploads", filePathName);
//        var uploadPath = Path.Combine(_env.ContentRootPath, "uploads", filePathName);

//        try
//        {
//            var fileModel = new FoodAvatar();

//            fileModel.FilePath = path;
//            fileModel.FileExtension = file.ContentType;
//            fileModel.FileName = fullFileName;

//            using (var fs = new FileStream(uploadPath, FileMode.CreateNew))
//            {
//                await file.CopyToAsync(fs, cancellationToken);
//            }

//            await _context.FoodAvatars.AddAsync(fileModel, cancellationToken);

//            return fileModel.Id;
//        }
//        catch (Exception e)
//        {
//            File.Delete(uploadPath);
//            throw;
//        }
//    }
//}
