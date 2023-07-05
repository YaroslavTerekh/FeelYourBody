using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class BaseProduct : BaseEntity
{
    public List<User> Users { get; set; } = new();

    public long Price { get; set; }
}
