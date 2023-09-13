using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class CoachingDetailsConfiguration : IEntityTypeConfiguration<CoachingDetails>
{
    public void Configure(EntityTypeBuilder<CoachingDetails> builder)
    {
        builder.HasOne(t => t.CoachingDetailsParent)
            .WithMany(t => t.Details)
            .HasForeignKey(t => t.CoachingDetailsParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
