using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Identity.Context;

public class AuthDbContext : IdentityDbContext<User, Role, int>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options ) : base(options)
    {
        
    }
}