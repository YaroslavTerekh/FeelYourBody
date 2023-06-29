using FYB.Data.Configurations;
using FYB.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FYB.Data.DbConnection;

public class DataContext : IdentityDbContext<User, ApplicationRole, Guid>
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }


    public DbSet<Coach> Coaches { get; set; }

    public DbSet<Coaching> Coachings { get; set; }

    public DbSet<CoachingVideo> CoachingVideos { get; set; }

    public DbSet<AppFile> Files { get; set; }

    public DbSet<CoachingDetails> CoachingDetails { get; set; }

    public DbSet<FAQ> FAQs { get; set; }

    public DbSet<Feedback> Feedbacks { get; set; }

    public DbSet<Food> Food { get; set; }

    public DbSet<FoodPoint> FoodPoints { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new FoodConfiguration());
    }
}
