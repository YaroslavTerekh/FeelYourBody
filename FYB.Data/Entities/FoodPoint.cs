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

    public Guid FoodId { get; set; }

    public Food Food { get; set; }
}
