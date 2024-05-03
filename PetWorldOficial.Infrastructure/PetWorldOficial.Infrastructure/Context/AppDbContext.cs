using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Infrastructure.IdentityEntities;

namespace PetWorldOficial.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}