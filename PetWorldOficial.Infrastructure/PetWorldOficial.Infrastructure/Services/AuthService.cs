using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Infrastructure.Services;

public class AuthService(
    IUserService _userService,
    UserManager<User> _userManager,
    SignInManager<User> _signInManager,
    RoleManager<Role> _roleManager,
    IMapper _mapper)
    : IAuthService
{
    public async Task<bool> Login(User user, string password)
    {
        if (!await _userManager.CheckPasswordAsync(user, password))
            return false;
        
        await _signInManager.SignInAsync(user, false);
        return true;
    }

    public async Task<User?> Register(RegisterUserViewModel model)
    {
        var user = _mapper.Map<User>(model);
        var createdUser = await _userManager.CreateAsync(user, model.Password);

        if (!createdUser.Succeeded)
            return user;

        var addedRole = await _userManager.AddToRoleAsync(user, ERole.User.ToString());

        if (!createdUser.Succeeded.Equals(addedRole.Succeeded))
            await _userManager.DeleteAsync(user);

        return user;
    }

    public async Task Logout() => await _signInManager.SignOutAsync();
}