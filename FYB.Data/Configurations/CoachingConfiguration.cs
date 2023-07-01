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
        //builder.HasOne(t => t.CoachingPhoto)
        //    .WithOne(t => t.Coaching)
        //    .HasForeignKey<Coaching>(t => t.CoachingPhotoId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder.HasMany(t => t.ExamplePhotos)
        //    .WithOne(t => t.CoachingList)
        //    .HasForeignKey(t => t.CoachingListId)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}
