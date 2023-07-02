using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class FoodPointDTO
{
    public string Title { get; set; }

    public string Description { get; set; }

    public long PortionMass { get; set; }
}
