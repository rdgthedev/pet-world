using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Login(User model, string password);
    Task<User?> Register(RegisterUserViewModel registerUser);
    Task Logout();
}