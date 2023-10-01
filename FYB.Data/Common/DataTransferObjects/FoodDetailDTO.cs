using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Common.DataTransferObjects;

public class FoodDetailDTO
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Detail { get; set; }
}
