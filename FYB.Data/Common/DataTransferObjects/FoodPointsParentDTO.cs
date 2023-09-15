using FYB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class FoodPointsParentDTO : BaseEntity
{
    public int DayNumber { get; set; }

    public List<FoodPointDTO> FoodPoints { get; set; }
}
