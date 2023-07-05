using FYB.Data.Common;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.CreateCoaching;

public class CreateCoachingCommand : IRequest
{
    public IFormFile CoachingPhoto { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public long Price { get; set; }

    public Guid CoachId { get; set; }
}