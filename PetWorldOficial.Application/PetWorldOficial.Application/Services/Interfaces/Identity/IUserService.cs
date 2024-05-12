using PetWorldOficial.Application.DTOs.User.Output;

namespace PetWorldOficial.Application.Services.Interfaces.Identity;

public interface IUserService
{
    Task<List<OutputUserDto>> GetAll();
    Task<OutputUserDto> GetById(int id);
    Task<OutputUserDto> GetByUserName(string userName);
    Task<OutputUserDto> UserExists(
        string userName, 
        string? document = null,
        string? email = null,
        string? phoneNumber = null);
}