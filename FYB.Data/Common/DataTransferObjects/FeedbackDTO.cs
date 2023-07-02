﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class FeedbackDTO
{
    public Guid Id { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public List<AppFileDTO> Photos { get; set; }
}
