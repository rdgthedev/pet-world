﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.Commands.Role;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Role;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public RoleService(
        RoleManager<Role> roleManager,
        UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<RoleDetailsViewModel?> GetByName(string name)
    {
        if (string.IsNullOrEmpty(name.Trim()))
            return null!;

        return await _roleManager.Roles.AsNoTracking().Select(role => new RoleDetailsViewModel(role.Name!))
            .FirstOrDefaultAsync(r => r.Name == name);
    }
    
    public async Task<List<string>> GetRolesByUserAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList();
    }
    public async Task<bool> AddRoleAsync(RegisterRoleCommand role)
    {
        var roleResult = await _roleManager
            .Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == role.Name);

        IdentityResult? result = null;

        if (roleResult == null)
            result = await _roleManager.CreateAsync(new Role(role.Name));

        return result!.Succeeded;
    }
}