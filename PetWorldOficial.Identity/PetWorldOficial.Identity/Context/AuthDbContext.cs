using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Identity.Mappings;

namespace PetWorldOficial.Identity.Context;

public class AuthDbContext : IdentityDbContext<User, Role, int>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options ) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserMap());
    }
}