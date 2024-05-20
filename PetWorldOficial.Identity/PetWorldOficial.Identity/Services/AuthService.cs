using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task Login(UserLoginDTO user)
    {
        var userResult = await _userManager.
            Users.
            AsNoTracking().
            FirstOrDefaultAsync(u => u.UserName == user.UserName);
        
        if(userResult is null) throw new LoginInvalidException("Usuário ou Senha inválidos!");

        if (!await _userManager.CheckPasswordAsync(userResult, user.Password))
            throw new LoginInvalidException("Usuário ou Senha inválidos!"); 
        
        await _signInManager.SignInAsync(userResult, false);
    }
    
    public async Task<bool> Register(UserRegisterDTO model)
    {
        var user = new User(
            model.Name,
            model.UserName,
            model.Gender.ToString(),
            model.BirthDate,
            model.Document,
            model.Email,
            model.PhoneNumber,
            model.Street,
            model.Number,
            model.PostalCode,
            model.Neighborhood,
            model.Complement,
            model.City,
            model.State);
        
        var createdUser = await _userManager.CreateAsync(user, model.Password);
        var addedRole = await _userManager.AddToRoleAsync(user, ERole.User.ToString());

        return createdUser.Equals(addedRole);
    }

    public async Task Logout() => await _signInManager.SignOutAsync();
}