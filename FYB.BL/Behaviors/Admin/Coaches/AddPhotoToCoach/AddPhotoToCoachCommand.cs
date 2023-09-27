using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.AddPhotoToCoach;

public class AddPhotoToCoachCommand : IRequest
{
    public Guid Id { get; set; }
    public string FileName { get; set; }

    public List<IFormFile> Files { get; set; }
}
