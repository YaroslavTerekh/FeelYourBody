using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class FoodPointParent : BaseEntity
{
    public int DayNumber { get; set; }

    // public List<FoodPoint> FoodPoints { get; set; }

    public Guid FoodId { get; set; }

    public Food Food { get; set; }
}
