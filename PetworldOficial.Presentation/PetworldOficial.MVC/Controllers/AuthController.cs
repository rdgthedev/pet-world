using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthController(
        IAuthService authService, 
        IUserService userService,
        IMapper mapper)
    {
        _authService = authService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserLoginDTO userLoginDTO)
    {
        if (!ModelState.IsValid) return View("Login");
        
        try
        {
            await _userService.GetByUserName(userLoginDTO.UserName);
            await _authService.Login(userLoginDTO);
            return RedirectToAction("Index", "Home");
        }
        catch (LoginInvalidException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("Login");
        }
    }
    
    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterUserDTO registerUserDto)
    {
        if (!ModelState.IsValid) return View(registerUserDto);

        try
        {
            // var user = _mapper.Map<User>(registerUserDto);
            // // if (await _userService.UserExists(user)) throw new UserAlreadyExistsException("Usuário já existe!");
            
            if (!await _authService.Register(registerUserDto))
            {
                TempData["ErrorMessage"] = "Não foi possível cadastrar o usuário!";
                return View();
            }

            TempData["SuccessMessage"] = "Usuário criado com sucesso!";

            return View();
        }
        catch (UserAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View("Register");
        }
        catch(Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível realizar o seu cadastro!";
            return View();
        }
    }
    
    [HttpGet]
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
