using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class CoachingConfiguration : IEntityTypeConfiguration<Coaching>
{
    public void Configure(EntityTypeBuilder<Coaching> builder)
    {
        builder.HasOne(t => t.CoachingPhoto)
            .WithOne(t => t.Coaching)
            .HasForeignKey<AppFile>(t => t.CoachingId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
