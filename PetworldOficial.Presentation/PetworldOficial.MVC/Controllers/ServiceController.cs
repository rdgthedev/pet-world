using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Queries.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetworldOficial.MVC.Utils;

namespace PetworldOficial.MVC.Controllers;

public class ServiceController(
    IWebHostEnvironment webHostEnvironment,
    ICategoryService categoryService,
    IMapper mapper,
    IMediator mediator,
    IMemoryCache cache) : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "-1";

        try
        {
            var services = await mediator.Send(new GetAllServicesQuery(), cancellationToken);
            return View(services);
        }
        catch (ServiceNotFoundException e)
        {
            TempData["NotFoundService"] = e.Message;
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
            var result = await mediator.Send(new GetServiceByIdQuery(id), cancellationToken);

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
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateServiceCommand(), cancellationToken);
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateServiceCommand command,
        CancellationToken cancellationToken)
    {
        if (cache.TryGetValue("ServiceCategories", out IEnumerable<CategoryDetailsViewModel> categories))
            command.Categories = categories;

        if (!ModelState.IsValid)
            return View(command);

        try
        {
            command.BaseUrl = webHostEnvironment.ContentRootPath;
            var result = await mediator.Send(command, cancellationToken);

            TempData["SuccessMessage"] = result.Message;

            return RedirectToAction("Index");
        }
        catch (ServiceAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View(command);
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View(command);
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
            var result = await mediator.Send(new UpdateServiceCommand { Id = id }, cancellationToken);

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
        command.Category =
            mapper.Map<Category>(await categoryService.GetById(command.CategoryId!.Value, cancellationToken));
        command.Categories = await categoryService.GetAllServiceCategories(cancellationToken);

        if (!ModelState.IsValid)
            return View(command);

        try
        {
            command.File = file!;
            command.RootPath = webHostEnvironment.WebRootPath;

            var result = await mediator.Send(command, cancellationToken);

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
            var result = await mediator.Send(new DeleteServiceCommand { Id = id }, cancellationToken);
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
    public async Task<IActionResult> Delete(
        DeleteServiceCommand command,
        IFormFile? file,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            var result = await mediator.Send(command, cancellationToken);
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