using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.DTOs.Role.Input;
using PetWorldOficial.Application.DTOs.User.Output;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;
    
    public RoleService(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<OutputRoleDTO> GetByName(string name)
    {
        return await _roleManager.Roles.AsNoTracking().Select(role => new OutputRoleDTO
        {
            Name = role.Name
        }).FirstOrDefaultAsync(r => r.Name == name);
    }
    public async Task<bool> AddRoleAsync(RoleRegisterDTO role)
    {
        var roleResult = await _roleManager
            .Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == role.Name);
        
        IdentityResult? result = null;
       
        if(roleResult == null)
            result = await _roleManager.CreateAsync(new Role(role.Name));
        
        return result!.Succeeded;
    }
}