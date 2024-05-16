using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetworldOficial.MVC.Controllers;

public class ServiceController : Controller
{
    private readonly IServiceRepository _serviceRepository;
    
    public ServiceController(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    [HttpGet("v1/services")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var services = await _serviceRepository.GetAll();

            if (services is null) throw new NotFoundException("Nenhum serviço encontrado!");

            return View(services);
        }
        catch(NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet("v1/products/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var service = await _serviceRepository.GetById(id);

            if (service is null) throw new NotFoundException("Serviço não encontrado!");

            return View(service);
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Error", "Home");
        }
    }
    
}