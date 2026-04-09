using Baytak.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Baytak.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Baytak.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Notification> Notifications { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);


        foreach (var relationship in builder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);
    //    builder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

    //    builder.Entity<ApplicationUser>().ToTable("Users");
    //}
}
