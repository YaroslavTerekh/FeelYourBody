﻿using FYB.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class CoachingDetails : BaseEntity
{
    public CoachingDetailtsIconEnum Icon { get; set; }

    public string Detail { get; set; }

    public Guid CoachingId { get; set; }

    public Coaching Coaching { get; set; }
}