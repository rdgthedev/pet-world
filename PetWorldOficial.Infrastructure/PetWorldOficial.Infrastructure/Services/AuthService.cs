using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Infrastructure.Services;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager) : IAuthService
{
    public async Task<User> Login(LoginUserCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email.Trim().ToLower());

        if (user is null)
            throw new LoginInvalidException("Login ou senha inválidos!");

        if (!await userManager.CheckPasswordAsync(user, command.Password))
            return null!;

        await signInManager.SignInAsync(user, false);
        return user;
    }

    public async Task<User?> Register(User user, string? role, string password)
    {
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return null!;

        await userManager.AddToRoleAsync(user, role ?? ERole.User.ToString());

        if (!result.Succeeded)
        {
            await userManager.DeleteAsync(user);
            return null!;
        }

        var name = user.Name.Split(' ');
        
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, name[0]));
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email!));

        // await signInManager.SignInAsync(user, false);
        return user;
    }

    public async Task Logout() => await signInManager.SignOutAsync();
    public async Task SignIn(User user) => await signInManager.SignInAsync(user, false);
}