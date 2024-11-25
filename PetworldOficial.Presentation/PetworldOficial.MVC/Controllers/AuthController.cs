using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class AuthController(
    IMapper _mapper,
    IMediator mediator) : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(
        [FromForm] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            await mediator.Send(command, cancellationToken);
            return RedirectToAction("Index", "Home");
        }
        catch (LoginInvalidException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return View(command);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            var errors = await mediator.Send(command, cancellationToken);


            if (errors.Any())
            {
                foreach (var error in errors)
                    ModelState.AddModelError(string.Empty, error);

                return View(command);
            }

            TempData["SuccessMessage"] = "Cadastrado com sucesso!";

            if (User.IsInRole(ERole.Admin.ToString()))
                return RedirectToAction("Index", "User");
            return RedirectToAction("Index", "Home");
        }
        catch (UserAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (UnableToRegisterUserException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível realizar o seu cadastro!";
        }

        return View(command);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(new LogoutUserCommand(), cancellationToken);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Occoreu um erro interno!";
            return RedirectToPage("Error");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(
        ForgotPasswordCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View();

        var result = await mediator.Send(command, cancellationToken);
        TempData["SuccessMessage"] = result.message;
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(
        [FromQuery] string email,
        [FromQuery] string token)
    {
        return View(new ResetPasswordCommand { Email = email, Token = token });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(
        ResetPasswordCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            var result = await mediator.Send(command, cancellationToken);
            // TempData["SuccessMessage"] = result.
            return RedirectToAction("Login");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}