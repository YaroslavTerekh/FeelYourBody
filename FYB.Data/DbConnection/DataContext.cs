using FYB.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.DbConnection;

public class DataContext : IdentityDbContext<User, ApplicationRole, Guid>
{
    //public DataContext()
    //{

    //}
    public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }


    public DbSet<User> Users { get; set; }

    public DbSet<Coach> Coaches { get; set; }

    public DbSet<Coaching> Coachings { get; set; }

    public DbSet<CoachingVideo> CoachingVideos { get; set; }

    public DbSet<AppFile> Files { get; set; }


    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    if (!options.IsConfigured)
    //    {
    //        options.UseSqlServer("Server=DESKTOP-CECDKOC\\SQLEXPRESS;Database=FeelYourBodyDB11;Trusted_Connection=True;TrustServerCertificate=True");
    //    }
    //}
}
