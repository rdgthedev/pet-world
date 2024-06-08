using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class UserController(
    IUserService _userService,
    IMapper _mapper) : Controller
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var users = await _userService.GetAll();

            if (users is null)
                throw new UserNotFoundException("Nenhum usuário encontrado!");

            return View(users);
        }
        catch (UserNotFoundException e)
        {
            TempData["UserNotFoundMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return View();
    }

    // [HttpGet]
    // public async Task<IActionResult> Update(int id)
    // {
    //     try
    //     {
    //         var user = await _userService.GetById(id);
    //
    //         if (user is null)
    //             throw new UserNotFoundException("Usuário não encontrado!");
    //
    //         return View(new UpdateUserViewModel
    //         {
    //             Id = user.Id,
    //             Name = user.Name,
    //             UserName = user.UserName,
    //             Gender = user.Gender,
    //             Document = user.Document,
    //             BirthDate = user.BirthDate!,
    //             Email = user.Email,
    //             PhoneNumber = user.PhoneNumber,
    //             Street = user.Street,
    //             Number = user.Number,
    //             Complement = user.Complement,
    //             PostalCode = user.PostalCode,
    //             Neighborhood = user.Neighborhood,
    //             City = user.City,
    //             State = user.State,
    //         });
    //     }
    //     catch (UserNotFoundException e)
    //     {
    //         TempData["ErrorMessage"] = e.Message;
    //     }
    //     catch (Exception)
    //     {
    //         TempData["ErrorMessage"] = "Ocorreu um erro interno!";
    //     }
    //
    //     return View("Index");
    // }

    // [HttpPost]
    // public async Task<IActionResult> Update([FromForm] UpdateUserViewModel model)
    // {
    //     if (!ModelState.IsValid)
    //         return View(model);
    //
    //     try
    //     {
    //         var user = await _userService.GetById(model.Id);
    //
    //         if (user is null)
    //             throw new UserNotFoundException("Usuário não encontrado!");
    //
    //         _mapper.Map(model, user);
    //         await _userService.Update(user);
    //         TempData["ErrorMessage"] = "Usuário alterado com sucesso!";
    //         
    //         return RedirectToAction("Index");
    //     }
    //     catch (UserNotFoundException e)
    //     {
    //         TempData["ErrorMessage"] = e.Message;
    //     }
    //     catch (Exception)
    //     {
    //         TempData["ErrorMessage"] = "Ocorreu um erro interno!";
    //         return RedirectToAction("Index");
    //     }
    // }
}