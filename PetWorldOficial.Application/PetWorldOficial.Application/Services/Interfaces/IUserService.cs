using PetWorldOficial.Application.DTOs.User.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<OutputUserDto>> GetAll();
    Task<OutputUserDto> GetById(int id);
    Task<OutputUserDto> GetByUserName(string userName);
    Task<bool> UserExists(User user);
}