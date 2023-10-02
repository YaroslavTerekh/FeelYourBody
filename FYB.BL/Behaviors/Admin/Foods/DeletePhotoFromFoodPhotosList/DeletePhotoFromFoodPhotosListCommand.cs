using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.DeletePhotoFromFoodPhotosList;

public class DeletePhotoFromFoodPhotosListCommand : IRequest
{
    public Guid Id { get; set; }

    public DeletePhotoFromFoodPhotosListCommand(Guid id) => Id = id;
}
