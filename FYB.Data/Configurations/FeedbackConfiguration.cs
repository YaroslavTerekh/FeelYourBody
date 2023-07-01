using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        //builder.HasMany(t => t.Photos)
        //    .WithOne(t => t.Feedback)
        //    .HasForeignKey(t => t.FeedBackId)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}
