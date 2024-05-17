using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Identity.IdentityEntities;

namespace PetWorldOficial.Identity.Context;

public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options ) : base(options)
    {
        
    }
}