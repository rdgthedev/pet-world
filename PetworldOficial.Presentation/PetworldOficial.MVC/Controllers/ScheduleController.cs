using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;
using PetworldOficial.MVC.ViewModels.Service;

namespace PetworldOficial.MVC.Controllers;

public class ScheduleController : Controller
{
    private readonly IServiceService _serviceService;
    
    public ScheduleController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            return View(new ScheduleViewModel { Services = await _serviceService.GetAll() });
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
    public async Task<IActionResult> Schedule()
    {
        if (!ModelState.IsValid) return View();
        
        try
        {
            
            return View(await _serviceService.GetAll());
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