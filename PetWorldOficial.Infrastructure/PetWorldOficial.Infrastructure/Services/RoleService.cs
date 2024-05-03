using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.DTOs.Input;
using PetWorldOficial.Application.DTOs.Output;
using PetWorldOficial.Application.Services.Identity;
using PetWorldOficial.Infrastructure.IdentityEntities;

namespace PetWorldOficial.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    
    public RoleService(RoleManager<ApplicationRole> roleManager)
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
            result = await _roleManager.CreateAsync(new ApplicationRole(role.Name));
        
        return result!.Succeeded;
    }
}