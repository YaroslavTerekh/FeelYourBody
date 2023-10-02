using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.AddPhotoToFood;

public class AddPhotoToFoodCommand : IRequest
{
    public Guid FoodId { get; set; }

    public IFormFile File { get; set; }

    public string FileName { get; set; }
}
