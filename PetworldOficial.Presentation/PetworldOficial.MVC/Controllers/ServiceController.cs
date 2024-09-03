using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Service;
using PetWorldOficial.Domain.Exceptions;

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
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var services = await _serviceService.GetAll(cancellationToken);
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
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var service = await _serviceService.GetById(id, cancellationToken);
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
    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            var serviceResult = await _serviceService.GetByName(model.Name, cancellationToken);

            if (serviceResult != null)
                throw new ServiceAlreadyExistsException("Serviço já existe!");

            var imageUrl = _imageService.GenerateImageName(model.File, _webHostEnvironment.WebRootPath);
            await _imageService.SaveImage(model.File, _webHostEnvironment.WebRootPath, imageUrl);

            await _serviceService.Create(new CreateServiceDTO(model.Name, (double)model.Price!, imageUrl), cancellationToken);
            TempData["SuccessMessage"] = "Serviço cadastrado com sucesso!";

            return RedirectToAction("Index");
        }
        catch (ServiceAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
    {
        try
        {
            var service = await _serviceService.GetById(id, cancellationToken);

            if (service is null)
                throw new ServiceNotFoundException("Serviço não encontrado!");


            return View(new UpdateServiceViewModel
            {
                Name = service.Name,
                Price = service.Price,
                ImageUrl = service.ImageUrl
            });
        }
        catch (ServiceNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(
        UpdateServiceViewModel model,
        IFormFile? file,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var service = await _serviceService.GetById(model.Id, cancellationToken);

            if (service is null)
                throw new ServiceNotFoundException("Serviço não encontrado!");

            switch (file)
            {
                case not null:
                    model.ImageUrl = _imageService.GenerateImageName(file, _webHostEnvironment.WebRootPath);
                    await _imageService.SaveImage(file, _webHostEnvironment.WebRootPath, model.ImageUrl);
                    break;
                default:
                    model.ImageUrl = service.ImageUrl;
                    break;
            }

            await _serviceService.Update(model, cancellationToken);
            TempData["SuccessMessage"] = "Serviço alterado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (ServiceNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro!";
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var service = await _serviceService.GetById(id, cancellationToken);

            if (service is null)
                throw new ServiceNotFoundException("Serviço não encontrado!");


            return View(new DeleteServiceViewModel
            {
                Name = service.Name,
                Price = service.Price,
                ImageUrl = service.ImageUrl
            });
        }
        catch (ServiceNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(
        DeleteServiceViewModel model,
        IFormFile? file,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var service = await _serviceService.GetById(model.Id, cancellationToken);

            if (service is null)
                throw new ServiceNotFoundException("Serviço não encontrado!");

            switch (file)
            {
                case not null:
                    model.ImageUrl = _imageService.GenerateImageName(file, _webHostEnvironment.WebRootPath);
                    await _imageService.SaveImage(file, _webHostEnvironment.WebRootPath, model.ImageUrl);
                    break;
                default:
                    model.ImageUrl = service.ImageUrl;
                    break;
            }

            await _serviceService.Delete(model, cancellationToken);
            TempData["SuccessMessage"] = "Serviço deletado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (ServiceNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro!";
        }

        return RedirectToAction("Index");
    }
}