using PetWorldOficial.Application.DTOs;
using PetWorldOficial.Application.DTOs.Input;
using PetWorldOficial.Application.DTOs.Output;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Services.Identity;

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