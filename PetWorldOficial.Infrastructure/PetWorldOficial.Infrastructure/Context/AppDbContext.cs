using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}