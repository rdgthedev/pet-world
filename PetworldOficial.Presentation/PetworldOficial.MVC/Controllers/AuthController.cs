using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class AuthController(
    IAuthService _authService,
    IUserService _userService,
    IMapper _mapper)
    : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var user = await _userService.GetByUserName(model.UserName);

            if (user is null)
                throw new LoginInvalidException("Login ou senha inválidos!");

            if (!await _authService.Login(_mapper.Map<User>(user), model.Password))
                throw new LoginInvalidException("Login ou senha inválidos!");

            return RedirectToAction("Index", "Home");
        }
        catch (LoginInvalidException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }
        
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterUserViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            if (await _userService.UserExists(_mapper.Map<User>(model)))
                throw new UserAlreadyExistsException("Usuário já existe!");

            var user = await _authService.Register(model);

            if (user is null)
                throw new UnableToRegisterUserException("Não foi possível registrar o usuário!");
            
            
            await _authService.Login(user, model.Password);
            return RedirectToAction("Index", "Home");
        }
        catch (UserAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (UnableToRegisterUserException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível realizar o seu cadastro!";
        }

        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _authService.Logout();
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Occoreu um erro interno!";
            return RedirectToPage("Error");
        }
    }
}