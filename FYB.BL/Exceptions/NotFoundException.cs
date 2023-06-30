using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Exceptions;

public class NotFoundException : Exception
{
    public string Description { get; set; }

    public NotFoundException(string description = "Не знайдено")
    {
        Description = description;
    }
}
