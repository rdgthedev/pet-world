using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Animal.Output;
using PetWorldOficial.Application.DTOs.Schedule.Output;
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
    private readonly IScheduleService _scheduleService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public SchedullingController(
        IServiceService serviceService,
        IAnimalService animalService,
        IUserService userService,
        IScheduleService scheduleService,
        IMapper mapper)
    {
        _serviceService = serviceService;
        _animalService = animalService;
        _userService = userService;
        _scheduleService = scheduleService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            return View(_mapper.Map<IEnumerable<Service>>(await _serviceService.GetAll()));
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
            
            var service = _mapper.Map<Service>(await _serviceService.GetById(id));
            
            return View(new ScheduleRegisterViewModel{ Animal = _mapper.Map<Animal>(animal), Service = service });
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
    
    [HttpPost]
    public async Task<IActionResult> Schedule(ScheduleRegisterViewModel scheduleRegisterViewModel)
    {
        if (!ModelState.IsValid) return View();
        
        try
        {
            if (!await _scheduleService.DateExists(scheduleRegisterViewModel.Date))
                throw new DateAlreadyExistsException("Data já preenchida, escolha outra data!");

            // await _scheduleService.CreateAsync();
            
            return View();
        }
        catch (DateAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
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