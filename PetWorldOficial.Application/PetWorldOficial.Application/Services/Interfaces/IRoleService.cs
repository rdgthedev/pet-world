using PetWorldOficial.Application.Commands.Role;
using PetWorldOficial.Application.ViewModels.Role;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IRoleService
{
    Task<RoleDetailsViewModel?> GetByName(string name);
    Task<bool> AddRoleAsync(RegisterRoleCommand role);
    Task<List<string>> GetRolesByUserAsync(User user);
}