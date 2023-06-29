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
}
