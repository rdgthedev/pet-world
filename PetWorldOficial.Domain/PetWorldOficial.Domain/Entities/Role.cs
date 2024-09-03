using Microsoft.AspNetCore.Identity;

namespace PetWorldOficial.Domain.Entities;

public class Role(string name) : IdentityRole<int>(name)
{
}