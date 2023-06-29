using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class Food : BaseEntity
{
    public Guid CoachingId { get; set; }

    public Coaching Coaching { get; set; }

    public List<FoodPoint> FoodPoints { get; set; } = new();
}
