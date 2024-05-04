using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.DTOs.Input;
using PetWorldOficial.Application.Services.Identity;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Infrastructure.IdentityEntities;

namespace PetWorldOficial.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserService _userService;
    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserService userService,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
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
        var created = false;
        
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

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded){ created = await _userService.AddToRole(model, ERole.User); }
        return created;
    }

    public async Task Logout() => await _signInManager.SignOutAsync();
}