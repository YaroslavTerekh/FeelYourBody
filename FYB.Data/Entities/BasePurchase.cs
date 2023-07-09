using FYB.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Entities;

public class BasePurchase : BaseEntity
{
    public string OrderId { get; set; }

    public DateTime ExpireDate { get; set; }

    public bool IsExpired { get; set; }

    public PurchaseProductType ProductType { get; set; }

    public Guid ProductId { get; set; }

    public User User { get; set; }

    public Guid UserId { get; set; }
}
