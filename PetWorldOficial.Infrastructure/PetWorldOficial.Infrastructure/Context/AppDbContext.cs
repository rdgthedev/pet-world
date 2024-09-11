using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Schedulling> Schedullings { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // builder.Entity<User>().HasData(new User(
        //     "Admin",
        //     "admin",
        //     EGender.Male.ToString(),
        //     DateTime.Now,
        //     "12341278921478912",
        //     "admin@gmail.com",
        //     "123817389163781",
        //     "a",
        //     1,
        //     "a",
        //     "a",
        //     "a",
        //     "a",
        //     "a"));

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}