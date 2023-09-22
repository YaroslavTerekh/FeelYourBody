using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class CoachConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.HasMany(t => t.Photos)
            .WithOne(t => t.Coach)
            .HasForeignKey(t => t.CoachId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(t => t.CoachDetails)
            .WithOne(t => t.Coach)
            .HasForeignKey(t => t.CoachId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
