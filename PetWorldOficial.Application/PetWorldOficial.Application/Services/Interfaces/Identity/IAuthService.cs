using PetWorldOficial.Application.DTOs.User.Input;

namespace PetWorldOficial.Application.Services.Interfaces.Identity;

public interface IAuthService
{
    Task Login(UserLoginDTO user);
    Task<bool> Register(UserRegisterDTO user);
    Task Logout();
}