using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Identity.IdentityEntities;

namespace PetWorldOficial.Identity.Context;

public class IdentityDbContext : IdentityDbContext<User, Role, int>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options ) : base(options)
    {
        
    }
}