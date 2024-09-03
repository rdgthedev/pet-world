using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Infrastructure.Services;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager) : IAuthService
{
    public async Task<bool> Login(User user, string password)
    {
        if (!await userManager.CheckPasswordAsync(user, password))
            return false;

        await signInManager.SignInAsync(user, false);
        return true;
    }

    public async Task<bool> Register(User user, string password)
    {
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return false;

        await userManager.AddToRoleAsync(user, ERole.Admin.ToString());

        if (!result.Succeeded)
        {
            await userManager.DeleteAsync(user);
            return false;
        }

        return true;
    }

    public async Task Logout() => await signInManager.SignOutAsync();
}