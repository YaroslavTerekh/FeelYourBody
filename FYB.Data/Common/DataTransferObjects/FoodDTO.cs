﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class FoodDTO
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Guid? CoachingId { get; set; }

    public List<FoodPointDTO> FoodPoints { get; set; } = new();
}
