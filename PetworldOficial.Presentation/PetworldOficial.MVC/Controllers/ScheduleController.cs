using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Schedule.Input;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetworldOficial.MVC.Models.Schedule;

namespace PetworldOficial.MVC.Controllers;

public class ScheduleController : Controller
{
    private readonly IServiceService _serviceService;
    private readonly IAnimalService _animalService;
    private readonly IScheduleService _scheduleService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public ScheduleController(
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
            var animals = await _animalService.GetByOwner(user!.Id);
            var service = _mapper.Map<Service>(await _serviceService.GetById(id));
            return View(new RegisterScheduleViewModel
            { 
                Animals = _mapper.Map<IEnumerable<Animal>>(animals), 
                Service = service 
            });
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
    public async Task<IActionResult> Schedule(RegisterScheduleViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        try
        {
            if (await _scheduleService.BusySchedule(model.Date))
                throw new BusyScheduleException("Agenda lotada para esta data!");
            
            if (await _scheduleService.MaximumBookingsPerAnimalExceeded(model.AnimalId, model.Date, model.Time))
                throw new MaximumBookingsPerAnimalExceededException("Verifique sua agenda seus serviços agendados devem " +
                                                                    "ter pelo menos a diferença de uma hora de um para o outro!");
                
            await _scheduleService.CreateAsync(new RegisterScheduleDTO(
                model.AnimalId,
                model.ServiceId,
                model.Date,
                model.Time,
                model.Observation));
            
            TempData["SuccessMessage"] = "Agendamento realizado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (BusyScheduleException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View("Index");
        }
        catch (MaximumBookingsPerAnimalExceededException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View("Index");
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View("Index");
        }
    }
}