using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Animal.Input;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class AnimalController : Controller
{
    private readonly IAnimalService _animalService;
    private readonly IUserService _userService;

    public AnimalController(
        IAnimalService animalService,
        IUserService userService)
    {
        _animalService = animalService;
        _userService = userService;
    }
    
    public IActionResult Index() => View();

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        try
        {
            var user = await _userService.GetByUserName(User.Identity.Name);
            return View(new RegisterAnimalDTO { UserId = user.Id });
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível realizar o cadastro do pet!";
            return View();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterAnimalDTO registerAnimalDto)
    {
        if (!ModelState.IsValid) return View(registerAnimalDto);

        try
        {
            await _animalService.Create(registerAnimalDto);
            TempData["SuccessMessage"] = "Pet cadastrado com sucesso!";
            return RedirectToAction("Index", "Schedule");
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível realizar o cadastro do pet!";
            return View();
        }
    }
}