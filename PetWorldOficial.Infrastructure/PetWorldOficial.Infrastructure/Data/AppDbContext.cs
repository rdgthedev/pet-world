using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Infrastructure.IdentityModels;

namespace PetWorldOficial.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Animal> Animals { get; set; }
}