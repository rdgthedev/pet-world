using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Input;
using PetWorldOficial.Application.Services.Identity;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Infrastructure.IdentityEntities;

namespace PetworldOficial.MVC.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserService _userService;

    public AuthController(
        IAuthService authService, 
        IUserService userService,
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _authService = authService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserLoginDTO userLoginDTO)
    {
        if (!ModelState.IsValid) return View("Login");
        
        try
        {
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
    public async Task<IActionResult> Register([FromForm] UserRegisterDTO userRegisterDTO)
    {
        if (!ModelState.IsValid) return View();
        
        try
        {
            var userResult = await _userService.UserExists(
                userRegisterDTO.UserName,
                userRegisterDTO.Document,
                userRegisterDTO.Email,
                userRegisterDTO.PhoneNumber);

            if (userResult != null) 
                throw new UserAlreadyExistsException("Usuário já existe, verifique os campos: " +
                                                     "Nome de Usuário, Documento, Email e Número de Telefone");

            if (!await _authService.Register(userRegisterDTO))
                ModelState.AddModelError(string.Empty,"Não foi possível cadastrar o usuário!");
            
            TempData["SuccessMessage"] = "Usuário criado com sucesso!";
            
            return View();
        }
        catch (UserAlreadyExistsException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("Register");
        }
    }
}
