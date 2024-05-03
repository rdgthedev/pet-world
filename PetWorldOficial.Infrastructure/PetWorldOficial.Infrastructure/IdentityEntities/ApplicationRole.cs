using Microsoft.AspNetCore.Identity;

namespace PetWorldOficial.Infrastructure.IdentityEntities;

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole(string name) : base(name)
    {
    }
}