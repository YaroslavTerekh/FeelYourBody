﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class CoachingDetailDTO
{
    public Guid Id { get; set; }

    public CoachingDetailtsIconEnum Icon { get; set; }

    public string Detail { get; set; }
}
