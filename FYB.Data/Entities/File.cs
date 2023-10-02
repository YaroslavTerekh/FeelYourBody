﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class AppFile : BaseFile
{
    public int? OrderId { get; set; }

    public Guid? FeedBackId { get; set; }

    public Feedback? Feedback { get; set; }

    public Guid? CoachingId { get; set; }

    public Coaching? Coaching { get; set; }

    public Guid? CoachingListId { get; set; }

    public Coaching? CoachingList { get; set; }

    public Guid? CoachId { get; set; }

    public Coach? Coach { get; set; }
}
