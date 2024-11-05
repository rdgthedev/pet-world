using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Login(LoginUserCommand command);
    Task<bool> Register(User user, string? role, string? password);
    Task Logout();
}