using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<bool> Login(User model, string password);
    Task<bool> Register(User user, string password);
    Task Logout();
}