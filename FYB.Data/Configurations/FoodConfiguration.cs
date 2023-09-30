using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class FoodConfiguration : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.HasOne(t => t.Coaching)
            .WithOne(t => t.Food)
            .HasForeignKey<Food>(t => t.CoachingId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(t => t.FoodPoints)
            .WithOne(t => t.Food)
            .HasForeignKey(t => t.FoodId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
