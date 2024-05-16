using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.ApplicationServices;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetworldOficial.MVC.Controllers;

public class ServiceController : Controller
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IImageService _imageService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMapper _mapper;
    
    public ServiceController(
        IServiceRepository serviceRepository,
        IImageService imageService,
        IWebHostEnvironment webHostEnvironment,
        IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _imageService = imageService;
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
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
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Error", "Home");
        }
    }
}