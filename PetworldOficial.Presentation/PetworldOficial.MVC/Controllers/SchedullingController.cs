using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetworldOficial.MVC.ViewModels.Schedule;

namespace PetworldOficial.MVC.Controllers;

public class SchedullingController : Controller
{
    private readonly IServiceService _serviceService;
    private readonly IAnimalService _animalService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public SchedullingController(
        IServiceService serviceService,
        IAnimalService animalService,
        IUserService userService,
        IMapper mapper)
    {
        _serviceService = serviceService;
        _animalService = animalService;
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            return View(new ScheduleServicesViewModel { Services = await _serviceService.GetAll() });
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Schedule(int id)
    {
        try
        {
            var user = await _userService.GetByUserName(User.Identity?.Name!);
            var animal = await _animalService.GetByOwner(user!.Id);

            if (animal == null)
            {
                return RedirectToAction();
            }
            
            
            var service = _mapper.Map<Service>(await _serviceService.GetById(id));
            return View(new ScheduleRegisterViewModel{ Service = service });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Schedule(ScheduleRegisterViewModel scheduleRegisterViewModel)
    {
        if (!ModelState.IsValid) return View();
        
        try
        {
            
            
            return View();
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View();
        }
    }
}