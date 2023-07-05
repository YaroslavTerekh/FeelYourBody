using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class FoodPointConfiguration : IEntityTypeConfiguration<FoodPoint>
{
    public void Configure(EntityTypeBuilder<FoodPoint> builder)
    {
        builder.HasOne(t => t.Food)
            .WithMany(t => t.FoodPoints)
            .HasForeignKey(t => t.FoodId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
