using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Queries.Animal;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Application.ViewModels.Race;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetworldOficial.MVC.Controllers;

public class AnimalController(
    IMediator mediator,
    IMemoryCache cache,
    IWebHostEnvironment webHostEnvironment,
    IAnimalRepository animalRepository,
    ICategoryService categoryService,
    IUserService userService) : Controller
{
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var animals = Enumerable.Empty<AnimalDetailsViewModel>();

        try
        {
            animals = await mediator.Send(new GetAllAnimalsQuery(User), cancellationToken);
            return View(animals);
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Login", "Auth");
        }
        catch (UnauthorizedUserException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Home");
        }
        catch (NotFoundException e)
        {
            TempData["NotFoundPetMessage"] = e.Message;
            return View(animals);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View(animals);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByOwnerId([FromQuery] int id, CancellationToken cancellationToken)
    {
        var animals = Enumerable.Empty<Animal>();

        try
        {
            animals = await animalRepository.GetByUserIdWithOwnerAndRaceAsync(id, cancellationToken);
            return Ok(animals);
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Login", "Auth");
        }
        catch (UnauthorizedUserException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Home");
        }
        catch (NotFoundException e)
        {
            TempData["NotFoundPetMessage"] = e.Message;
            return View(animals);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View(animals);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Register(CancellationToken cancellationToken)
    {
        RegisterAnimalCommand result = null!;

        try
        {
            result = await mediator.Send(new RegisterAnimalCommand { UserPrincipal = User }, cancellationToken);
            return View(result);
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Login", "Auth");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterAnimalCommand command, CancellationToken cancellationToken)
    {
        command.UserPrincipal = User;

        if (!ModelState.IsValid)
        {
            command.Categories = await categoryService.GetAllAnimalCategories(cancellationToken);

            if (command.AdminId.HasValue)
                command.Users =
                    await userService.GetAllUsersExceptCurrentAsync(command.AdminId!.Value, cancellationToken);

            return View(command);
        }

        try
        {
            command.BaseUrl = webHostEnvironment.ContentRootPath;
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (AnimalAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Login", "Auth");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível realizar o cadastro do pet!";
            return View();
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new UpdateAnimalCommand(id) { UserPrincipal = User }, cancellationToken);
            return View(result);
        }
        catch (AnimalNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAnimalCommand command, CancellationToken cancellationToken)
    {
        command.UserPrincipal = User;

        if (!ModelState.IsValid)
        {
            command.Categories = await categoryService.GetAllAnimalCategories(cancellationToken);
            return View(command);
        }

        try
        {
            command.BaseUrl = webHostEnvironment.ContentRootPath;
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new DeleteAnimalCommand(id) { UserPrincipal = User }, cancellationToken);
            return View(result);
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = $"Ocorreu um erro. {e.Message}";
            return RedirectToAction("Login", "Auth");
        }

        catch (AnimalNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }

        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteAnimalCommand command, CancellationToken cancellationToken)
    {
        command.UserPrincipal = User;

        if (!ModelState.IsValid)
            return View(command);

        DeleteAnimalCommand result = null!;
        try
        {
            result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index");
        }
    }
}