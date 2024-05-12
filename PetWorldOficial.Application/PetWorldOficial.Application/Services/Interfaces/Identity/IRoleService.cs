using PetWorldOficial.Application.DTOs.Role.Input;
using PetWorldOficial.Application.DTOs.User.Output;

namespace PetWorldOficial.Application.Services.Interfaces.Identity;

public interface IRoleService
{
    Task<OutputRoleDTO> GetByName(string name);
    Task<bool> AddRoleAsync(RoleRegisterDTO role);
}