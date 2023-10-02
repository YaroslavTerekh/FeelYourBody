using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class Food : BaseProduct
{
    public Guid? CoachingId { get; set; }

    public Coaching? Coaching { get; set; }

	public List<FoodDetail> FoodDetails { get; set; } = new();
    
    public List<FoodPoint> FoodPoints { get; set; } = new();

    public List<FoodPhoto> Photos { get; set; } = new();
}
