using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class Food : BaseProduct
{
    public string Title { get; set; }

    public string Description { get; set; }

    public Guid? CoachingId { get; set; }

    public Coaching? Coaching { get; set; }

    public List<FoodPoint> FoodPoints { get; set; } = new();
}
