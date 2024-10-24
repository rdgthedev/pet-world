﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Entities;
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
            await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "Cadastrado com sucesso!";
            return View();
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
}