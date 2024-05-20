using PetWorldOficial.Application.DTOs.User.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces.Identity;

public interface IUserService
{
    Task<List<OutputUserDto>> GetAll();
    Task<OutputUserDto> GetById(int id);
    Task<OutputUserDto> GetByUserName(string userName);
    Task<OutputUserDto> UserExists(User user);
}