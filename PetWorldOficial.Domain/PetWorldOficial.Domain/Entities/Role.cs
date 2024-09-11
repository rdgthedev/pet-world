using Microsoft.AspNetCore.Identity;

namespace PetWorldOficial.Domain.Entities;

public class Role : IdentityRole<int>
{
    public Role(string name) : base(name)
        => CreatedAt = DateTime.Now;

    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }
}