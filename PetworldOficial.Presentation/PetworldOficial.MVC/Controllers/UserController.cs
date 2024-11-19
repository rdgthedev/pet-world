using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Queries.User;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class UserController(
    IMediator mediator,
    IMapper mapper) : Controller
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var users = await mediator.Send(new GetAllClientsAndEmployeesQuery(User), cancellationToken);
            return View(users);
        }
        catch (UserNotFoundException e)
        {
            TempData["UserNotFoundMessage"] = e.Message;
        }
        catch (AccessFailureException e)
        {
            TempData["UserNotFoundMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return View();
    }

    [HttpGet]
    public IActionResult MyAccount()
    {
        return View();
    }
    

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return View(mapper.Map<UpdateUserCommand>(user));
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(
        [FromForm] UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "Usuário alterado com sucesso!";

            return RedirectToAction("Index");
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return View(mapper.Map<DeleteUserCommand>(user));
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(
        [FromForm] DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "Usuário deletado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (EmployeeHasPendingSchedulingsException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }
}