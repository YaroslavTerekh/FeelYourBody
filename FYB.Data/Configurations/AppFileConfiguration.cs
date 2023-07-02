using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Configurations;

public class AppFileConfiguration : IEntityTypeConfiguration<AppFile>
{
    public void Configure(EntityTypeBuilder<AppFile> builder)
    {
        builder.HasOne(t => t.Coaching)
            .WithOne(t => t.CoachingPhoto)
            .HasForeignKey<Coaching>(t => t.CoachingPhotoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.Coach)
            .WithOne(t => t.Avatar)
            .HasForeignKey<AppFile>(t => t.CoachId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.Feedback)
            .WithMany(t => t.Photos)
            .HasForeignKey(t => t.FeedBackId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.CoachingList)
            .WithMany(t => t.ExamplePhotos)
            .HasForeignKey(t => t.CoachingListId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasCheckConstraint("CK_Files_CoachId_Or_FeedBackId_Or_CoachingListId_Or_CoachingId",
            "CASE " +
            "WHEN CoachId IS NOT NULL THEN " +
            "(CASE WHEN FeedBackId IS NULL AND CoachingListId IS NULL AND CoachingId IS NULL THEN 1 ELSE 0 END) " +
            "WHEN FeedBackId IS NOT NULL THEN " +
            "(CASE WHEN CoachingListId IS NULL AND CoachingId IS NULL THEN 1 ELSE 0 END) " +
            "WHEN CoachingListId IS NOT NULL THEN " +
            "(CASE WHEN CoachingId IS NULL THEN 1 ELSE 0 END) " +
            "ELSE " +
            "1 " +
            "END = 1");
    }
}
