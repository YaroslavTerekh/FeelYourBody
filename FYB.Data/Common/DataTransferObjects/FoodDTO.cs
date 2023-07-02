using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class FoodDTO
{
    public List<FoodPointDTO> FoodPoints { get; set; } = new();
}
