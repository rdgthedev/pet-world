using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.ApplicationServices;
using PetworldOficial.MVC.ViewModels.Service;

namespace PetworldOficial.MVC.Controllers;

public class ServiceController : Controller
{
    private readonly IServiceService _serviceService;
    private readonly IImageService _imageService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMapper _mapper;

    public ServiceController(
        IServiceService serviceService,
        IImageService imageService,
        IWebHostEnvironment webHostEnvironment,
        IMapper mapper)
    {
        _serviceService = serviceService;
        _imageService = imageService;
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        try
        {
            var services = await _serviceService.GetAll();
            return View(services);
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

    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var service = await _serviceService.GetById(id);
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

    [HttpGet]
    [Authorize(Roles="Admin")]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            var serviceResult = await _serviceService.GetByName(model.Name);

            if (serviceResult != null) 
                throw new ServiceAlreadyExistsException("Serviço já existe!");

            var imageUrl = _imageService.GenerateImageName(model.File, _webHostEnvironment.WebRootPath);
            await _imageService.SaveImage(model.File, _webHostEnvironment.WebRootPath, imageUrl);

            await _serviceService.Create(new CreateServiceDTO(model.Name, (double)model.Price!, imageUrl));
            TempData["SuccessMessage"] = "Serviço cadastrado com sucesso!";

            return View();
        }
        catch (ServiceAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
    }
}