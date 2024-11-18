using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IAuthService
{
    Task<User?> Login(LoginUserCommand command);
    Task<User?> Register(User user, string? role, string? password);
    Task ResetPassword(User user, string token, string newPassword);
    Task<string> GenerateForgetPasswordTokenAsync(User user);
    Task Logout();
    Task SignIn(User user);
}