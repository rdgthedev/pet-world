﻿using Microsoft.AspNetCore.Identity;

namespace PetWorldOficial.Domain.Entities;

public class Role : IdentityRole<int>
{
    public Role(string name) : base(name)
    {
        
    }
}