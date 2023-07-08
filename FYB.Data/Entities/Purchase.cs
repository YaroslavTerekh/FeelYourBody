using FYB.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class Purchase<T> : BasePurchase
{
    public T Product { get; set; }
}
