using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Application.Services.Interfaces.Identity;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Infrastructure.IdentityEntities;

namespace PetWorldOficial.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task Login(UserLoginDTO user)
    {
        var userResult = await _userManager.
            Users.
            AsNoTracking().
            FirstOrDefaultAsync(u => u.UserName == user.UserName!);
        
        if(userResult is null) throw new LoginInvalidException("Usuário ou Senha inválidos!");

        if (!await _userManager.CheckPasswordAsync(userResult, user.Password))
            throw new LoginInvalidException("Usuário ou Senha inválidos!"); 
        
        await _signInManager.SignInAsync(userResult, false);
    }
    
    public async Task<bool> Register(UserRegisterDTO model)
    {
        var user = new ApplicationUser(
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
        
        var resultUserCreated = await _userManager.CreateAsync(user, model.Password);
        var resultRoleAdded = await _userManager.AddToRoleAsync(user, ERole.User.ToString());

        return resultUserCreated.Equals(resultRoleAdded);
    }

    public async Task Logout() => await _signInManager.SignOutAsync();
}