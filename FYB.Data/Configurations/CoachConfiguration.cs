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
        //builder.HasOne(t => t.Avatar)
        //    .WithOne(t => t.Coach)
        //    .HasForeignKey<Coach>(t => t.AvatarId)
        //    .OnDelete(DeleteBehavior.SetNull);
    }
}
