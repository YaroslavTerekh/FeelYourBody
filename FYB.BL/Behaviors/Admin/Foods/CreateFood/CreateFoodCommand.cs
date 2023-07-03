﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.CreateFood;

public class CreateFoodCommand : IRequest
{
    public string Title { get; set; }

    public string Description { get; set; }

    public long Price { get; set; } = 1;

    public Guid? CoachingId { get; set; }
}
