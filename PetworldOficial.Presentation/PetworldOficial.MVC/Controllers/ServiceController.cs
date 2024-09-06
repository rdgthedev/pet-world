using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Application.Queries.Service;
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
    private readonly IMediator _mediator;

    public ServiceController(
        IServiceService serviceService,
        IImageService imageService,
        IWebHostEnvironment webHostEnvironment,
        IMapper mapper,
        IMediator mediator)
    {
        _serviceService = serviceService;
        _imageService = imageService;
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var services = await _mediator.Send(new GetAllServicesQuery(), cancellationToken);
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
            var result = await _mediator.Send(new GetServiceByIdQuery(id), cancellationToken);

            return View(result);
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
    public async Task<IActionResult> Create(CreateServiceCommand command, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            var result = await _mediator.Send(command, cancellationToken);

            TempData["SuccessMessage"] = result.Message;

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
            var result = await _mediator.Send(new UpdateServiceCommand { Id = id }, cancellationToken);

            return View(result);
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

    [HttpPost]
    public async Task<IActionResult> Update(
        UpdateServiceCommand command,
        IFormFile? file,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            command.File = file!;
            command.RootPath = _webHostEnvironment.WebRootPath;

            var result = await _mediator.Send(command, cancellationToken);

            TempData["SuccessMessage"] = result.Message;
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
            var result = await _mediator.Send(new GetServiceByIdQuery(id), cancellationToken);


            return View(result);
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
        DeleteServiceCommand command,
        IFormFile? file,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
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