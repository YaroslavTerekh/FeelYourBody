using FYB.BL.Exceptions;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.DeletePhotoFromFoodPhotosList;

public class DeletePhotoFromFoodPhotosListHandler : IRequestHandler<DeletePhotoFromFoodPhotosListCommand>
{
    private readonly DataContext _context;

    public DeletePhotoFromFoodPhotosListHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePhotoFromFoodPhotosListCommand request, CancellationToken cancellationToken)
    {
        var avatar = await _context.FoodPhotos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (avatar is null)
            throw new NotFoundException();

        File.Delete(avatar.FilePath);
        _context.FoodPhotos.Remove(avatar);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
