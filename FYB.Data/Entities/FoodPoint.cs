using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class FoodPoint : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public long PortionMass { get; set; }

    public string CoockingMethod { get; set; }

    public FoodPointParent FoodPointsParent { get; set; }

    public Guid FoodPointsParentId { get; set; }
}
