using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class AnimalController(
    IAnimalService _animalService,
    IUserService _userService,
    IMapper _mapper) : Controller
{
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Index()
    {
        IEnumerable<AnimalDetailsViewModel?> animals = null!;

        try
        {
            var user = await _userService.GetByUserName(User.Identity?.Name!);

            if (user is null)
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

            var role = User.IsInRole(ERole.Admin.ToString()) ? ERole.Admin : ERole.User;
            
            animals = role switch
            {
                ERole.Admin => await _animalService.GetWithUser(),
                ERole.User => await _animalService.GetByUserId(user.Id)
            };

            var animalDetailsViewModels = animals.ToArray();

            return View(animalDetailsViewModels.Length != 0
                ? animalDetailsViewModels
                : throw new NotFoundException("Nenhum pet encontrado!"));
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Login", "Auth");
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
    public async Task<IActionResult> Register()
    {
        try
        {
            var user = await _userService.GetByUserName(User.Identity?.Name!);

            if (user is null)
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

            if (User.IsInRole(ERole.Admin.ToString()))
                return View(new CreateAnimalViewModel
                    { Users = _mapper.Map<IEnumerable<User>>(await _userService.GetAll()) });

            return View(new CreateAnimalViewModel { UserId = user.Id });
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Login", "Auth");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível realizar o cadastro do pet!";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateAnimalViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var user = await _userService.GetByUserName(User.Identity?.Name!);

            if (user is null)
                throw new UserNotFoundException("Usuário não encontrado!");

            var animals = await _animalService.GetByUserId(user.Id);

            if (animals.Any(a => a!.Name == model.Name))
                throw new AnimalAlreadyExistsException("Este pet já está cadastrado!");

            await _animalService.Create(model);
            TempData["SuccessMessage"] = "Pet cadastrado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (AnimalAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível realizar o cadastro do pet!";
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var user = await _userService.GetByUserName(User.Identity?.Name!);

            if (user is null)
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

            var animal = await _animalService.GetById(id);

            if (animal is null)
                throw new AnimalNotFoundException("Pet não encontrado!");

            return View(new UpdateAnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Gender = animal.Gender,
                Race = animal.Race,
                Species = animal.Species,
                UserId = user.Id
            });
        }
        catch (AnimalNotFoundException e)
        {
            TempData["ErrorMessage"] =
                $"{e.Message}. Ocorreu um erro, tente de novo ou faça novamente o login no site!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAnimalViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _animalService.Update(model);
            TempData["SuccessMessage"] = "Pet alterado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var user = await _userService.GetByUserName(User.Identity?.Name!);

            if (user is null)
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

            var animal = await _animalService.GetById(id);

            if (animal is null)
                throw new AnimalNotFoundException("Pet não encontrado!");

            return View(new DeleteAnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Gender = animal.Gender,
                Race = animal.Race,
                Species = animal.Species,
                UserId = user.Id
            });
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
    public async Task<IActionResult> Delete(DeleteAnimalViewModel model)
    {
        if (!ModelState.IsValid)
            throw new Exception();

        try
        {
            await _animalService.Delete(model);
            TempData["SuccessMessage"] = "Pet excluído com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index");
        }
    }
}