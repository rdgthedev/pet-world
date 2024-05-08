using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Infrastructure.IdentityEntities;
using PetWorldOficial.Infrastructure.Mappings;

namespace PetWorldOficial.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     builder.ApplyConfiguration(new ProductMap());
    //     builder.ApplyConfiguration(new SupplierMap());
    // }
}