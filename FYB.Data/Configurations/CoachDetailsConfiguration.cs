using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class CoachDetailsConfiguration : IEntityTypeConfiguration<CoachDetails>
{
    public void Configure(EntityTypeBuilder<CoachDetails> builder)
    {
        builder.HasOne(t => t.Coach)
            .WithMany(t => t.CoachDetails)
            .HasForeignKey(t => t.CoachId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
