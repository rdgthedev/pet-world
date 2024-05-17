using Microsoft.AspNetCore.Identity;

namespace PetWorldOficial.Identity.IdentityEntities;

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole(string name) : base(name)
    {
    }
}