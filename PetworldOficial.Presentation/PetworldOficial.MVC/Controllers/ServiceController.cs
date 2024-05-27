using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.ApplicationServices;

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
    public async Task<IActionResult> Index()
    {
        try
        {
            var services= await _serviceService.GetAll();
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

    [HttpGet]
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
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceDTO createServiceDto, IFormFile file)
    {
        if (!ModelState.IsValid)
        {
            if(file == null)
                ModelState.AddModelError(string.Empty, "A imagem é obrigatória!");
            
            return View(createServiceDto);
        }

        try
        {
            var serviceResult = await _serviceService.GetByName(createServiceDto.Name);
            
            if (serviceResult != null) throw new ServiceAlreadyExistsException("Serviço já existe!");

            createServiceDto.ImageUrl = _imageService.GenerateImageName(file, _webHostEnvironment.WebRootPath);
            await _imageService.SaveImage(file, _webHostEnvironment.WebRootPath, createServiceDto.ImageUrl);
            
            await _serviceService.Create(createServiceDto);
            TempData["SuccessMessage"] = "Serviço criado com sucesso!";
            ModelState.Clear();
            
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