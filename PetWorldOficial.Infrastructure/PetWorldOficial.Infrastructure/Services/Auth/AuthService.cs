using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Infrastructure.Services.Auth;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager) : IAuthService
{
    public async Task<User?> Login(LoginUserCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email.Trim().ToLower());

        if (user is null)
            throw new LoginInvalidException("Login ou senha inválidos!");

        if (!await userManager.CheckPasswordAsync(user, command.Password))
            return null!;

        await signInManager.SignInAsync(user, false);
        return user;
    }

    public async Task<IdentityResult?> Register(User user, string? role, string password)
    {
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return result;
        }

        await userManager.AddToRoleAsync(user, role ?? ERole.User.ToString());

        if (!result.Succeeded)
        {
            await userManager.DeleteAsync(user);
            return result;
        }

        var name = user.Name.Split(' ');

        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, name[0]));
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email!));

        return result;
    }

    public async Task ResetPassword(
        User user,
        string token,
        string newPassword)
    {
        var existingUser = await userManager.FindByEmailAsync(user.Email!);
        
        var isValid = await userManager.VerifyUserTokenAsync(
            user, 
            TokenOptions.DefaultProvider, 
            "ResetPassword", 
            token);

        if (!isValid)
            throw new InvalidTokenException("O token de redefinição de senha está inválido!");

        var result = await userManager.ResetPasswordAsync(existingUser!, token, newPassword);

        if (!result.Succeeded)
            throw new Exception("Ocorreu um erro ao tentar resetar sua senha!");
    }

    public Task<string> GenerateForgetPasswordTokenAsync(User user)
    {
        return userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task Logout() => await signInManager.SignOutAsync();
    public async Task SignIn(User user) => await signInManager.SignInAsync(user, false);
}