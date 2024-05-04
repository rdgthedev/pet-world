using PetWorldOficial.Application.DTOs.Input;

namespace PetWorldOficial.Application.Services.Identity;

public interface IAuthService
{
    Task Login(UserLoginDTO user);
    Task<bool> Register(UserRegisterDTO user);
    Task Logout();
}