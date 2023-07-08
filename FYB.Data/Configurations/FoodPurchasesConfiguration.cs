using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class FoodPurchasesConfiguration : IEntityTypeConfiguration<Purchase<Food>>
{
    public void Configure(EntityTypeBuilder<Purchase<Food>> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Navigation(t => t.Product).AutoInclude();
    }
}
