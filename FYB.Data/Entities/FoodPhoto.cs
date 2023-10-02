using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class FoodPhoto : BaseFile
{
    public Guid FoodId { get; set; }

    public Food Food { get; set; }

    public int? OrderId { get; set; }
}
