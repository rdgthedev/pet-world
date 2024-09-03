using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class ScheduleController(
    IServiceService _serviceService,
    IAnimalService _animalService,
    IUserService _userService,
    IScheduleService _scheduleService,
    IMapper _mapper) : Controller
{
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        IEnumerable<ScheduleDatailsViewModel?> schedules = null!;

        try
        {
            var user = await _userService.GetByUserNameAsync(User.Identity?.Name!, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Usuário não encontrado!");

            // if (!User.IsInRole(ERole.Admin.ToString()))
            //     schedules = await _scheduleService.GetAllByAnimalsIds(
            //         (await _animalService.GetByUserId(user.Id)).Select(s => s!.Id));
            // else
            //     schedules = await _scheduleService.GetAll(cancellationToken);

            return View(schedules ?? throw new ScheduleNotFoundException("Nenhum agendamento encontrado!"));
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = $"{e.Message} Ocorreu um erro ao tentar identificar o usuário," +
                                       $" tente fazer o login novamente no site.";
            return View(schedules);
        }
        catch (ScheduleNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View(schedules);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Desculpe, não foi possível mostrar os agendamentos!";
            return View(schedules);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Create(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.GetByUserNameAsync(User.Identity?.Name!, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Usuário não encontrado!");

            var animals = await _animalService.GetByUserId(user.Id, cancellationToken);

            if (animals is null)
                throw new AnimalNotFoundException("Nenhum pet encontrado!");

            var service = _mapper.Map<Service>(await _serviceService.GetById(id, cancellationToken));

            if (service is null)
                throw new ServiceNotFoundException("Nenhum serviço encontrado!");

            return View(new CreateScheduleViewModel
            {
                Animals = _mapper.Map<IEnumerable<Animal>>(animals),
                Service = service,
                UserId = user.Id
            });
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = $"{e.Message} Ocorreu um erro ao tentar identificar o usuário," +
                                       $" tente fazer o login novamente no site.";
        }
        catch (AnimalNotFoundException e)
        {
            TempData["ErrorMessage"] = $"{e.Message} Antes de agendar, cadastre o seu pet!";
        }
        catch (ServiceNotFoundException e)
        {
            TempData["ErrorMessage"] = $"{e.Message} Ocorreu um erro ao tentar ao identificar o serviço" +
                                       $", tente agendar novamente!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível completar seu agendamento!";
        }

        return RedirectToAction("Index", "Service");
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateScheduleViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            model.Animals = _mapper.Map<IEnumerable<Animal>>(await _animalService.GetByUserId(model.UserId, cancellationToken));
            model.Service = _mapper.Map<Service>(await _serviceService.GetById(model.ServiceId, cancellationToken));
            return View(model);
        }

        try
        {
            if (await _scheduleService.BusySchedule((DateTime)model.Date!, cancellationToken))
                throw new BusyScheduleException("Agenda lotada para esta data!");

            if (await _scheduleService.IsMaximumBookingsExceeded((DateTime)model.Date, (TimeSpan)model.Time!, cancellationToken))
                throw new MaximumBookingsPerAnimalExceededException("Horário não disponível!");

            await _scheduleService.Create(model, cancellationToken);
            TempData["SuccessMessage"] = "Agendamento realizado com sucesso!";
            return RedirectToAction("Index", "Service");
        }
        catch (BusyScheduleException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (MaximumBookingsPerAnimalExceededException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = $"{e.Message} Ocorreu um erro ao tentar identificar o usuário," +
                                       $" tente fazer o login novamente no site.";
        }
        catch (ServiceNotFoundException e)
        {
            TempData["ErrorMessage"] = $"{e.Message} Ocorreu um erro ao tentar ao identificar o serviço" +
                                       $", tente agendar novamente!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        model.Animals = _mapper.Map<IEnumerable<Animal>>(await _animalService.GetByUserId(model.UserId, cancellationToken));
        model.Service = _mapper.Map<Service>(await _serviceService.GetById(model.ServiceId, cancellationToken));
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
    {
        try
        {
            var schedule = await _scheduleService.GetByIdWithAnimalAndService(id, cancellationToken);

            if (schedule is null)
                throw new ScheduleNotFoundException("Agendamento não encontrado!");

            return View(new UpdateScheduleViewModel
            {
                Id = schedule.Id,
                Date = schedule.Date,
                Time = schedule.Time,
                Observation = schedule.Observation,
                Animal = schedule.Animal,
                Service = schedule.Service
            });
        }
        catch (ScheduleNotFoundException e)
        {
            TempData["ErrorMessage"] = $"{e.Message} Ocorreu um erro ao tentar ao identificar o agendamento" +
                                       $", tente agendar novamente!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível completar seu agendamento!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateScheduleViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var schedule = await _scheduleService.GetByIdWithAnimalAndService(model.Id, cancellationToken);
            model.Service = schedule!.Service;
            model.Animal = schedule.Animal;
            return View(model);
        }

        try
        {
            if (await _scheduleService.BusySchedule((DateTime)model.Date!, cancellationToken))
                throw new BusyScheduleException("Agenda lotada para esta data!");

            if (await _scheduleService.IsMaximumBookingsExceeded((DateTime)model.Date, (TimeSpan)model.Time!, cancellationToken))
                throw new MaximumBookingsPerAnimalExceededException("Horário não disponível!");

            await _scheduleService.Update(model, cancellationToken);
            TempData["SuccessMessage"] = "Agendamento alterado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (BusyScheduleException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (MaximumBookingsPerAnimalExceededException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno";
            return RedirectToAction("Index");
        }

        var scheduleDetails = await _scheduleService.GetByIdWithAnimalAndService(model.Id, cancellationToken);
        model.Service = scheduleDetails!.Service;
        model.Animal = scheduleDetails.Animal;
        return View(model);
    }


    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var schedule = await _scheduleService.GetByIdWithAnimalAndService(id, cancellationToken);

            if (schedule is null)
                throw new ScheduleNotFoundException("Agendamento não encontrado!");

            return View(new DeleteScheduleViewModel
            {
                Id = schedule.Id,
                AnimalId = schedule.Animal.Id,
                ServiceId = schedule.Service.Id,
                Service = schedule.Service,
                Animal = schedule.Animal,
                Date = schedule.Date,
                Time = schedule.Time
            });
        }
        catch (ScheduleNotFoundException e)
        {
            TempData["ErrorMessage"] = $"{e.Message} Ocorreu um erro ao tentar identificar o agendamento!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] DeleteScheduleViewModel model, CancellationToken cancellationToken)
    {
        try
        {
            await _scheduleService.Delete(model, cancellationToken);
            TempData["SuccessMessage"] = "Agendamento cancelado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno";
            return RedirectToAction("Index");
        }
    }
}