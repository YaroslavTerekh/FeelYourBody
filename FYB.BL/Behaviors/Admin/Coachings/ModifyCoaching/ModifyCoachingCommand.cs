using FYB.Data.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.ModifyCoaching;

public class ModifyCoachingCommand : IRequest
{
    public Guid Id { get; set; }

    public CoachingIcon Icon { get; set; }

    public IFormFile? CoachingPhoto { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public long Price { get; set; }

    public Guid CoachId { get; set; }

    public Guid? FoodId { get; set; }

    public long AccessDays { get; set; }
}