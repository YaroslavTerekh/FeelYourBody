using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class Coaching : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public Guid CoachId { get; set; }

    public Coach Coach { get; set; }

    public List<CoachingVideo> Videos { get; set; } = new();

    public List<User> Users { get; set; } = new();
}
