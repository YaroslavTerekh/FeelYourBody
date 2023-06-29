using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class Feedback : BaseEntity
{
    public string FeedbackText { get; set; }

    public Guid CoachingId { get; set; }

    public Coaching Coaching { get; set; }

    public List<AppFile> Photos { get; set; } = new();
}
