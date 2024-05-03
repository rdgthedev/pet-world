using PetWorldOficial.Application.DTOs.Input;
using PetWorldOficial.Application.DTOs.Output;

namespace PetWorldOficial.Application.Services.Identity;

public interface IRoleService
{
    Task<OutputRoleDTO> GetByName(string name);
    Task<bool> AddRoleAsync(RoleRegisterDTO role);
}