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
            .HasForeignKey<Coach>(t => t.AvatarId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
