using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Application.ViewModels.Service;
using PetWorldOficial.Domain.Exceptions;
using PetworldOficial.MVC.Utils;

namespace PetworldOficial.MVC.Controllers;

public class ScheduleController(
    IServiceService _serviceService,
    IAnimalService _animalService,
    IUserService _userService,
    IScheduleService _scheduleService,
    IMapper _mapper,
    IMediator mediator) : Controller
{
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        IEnumerable<ScheduleDetailsViewModel?> schedules = null!;

        try
        {
            schedules = await mediator.Send(new GetAllSchedulesQuery(User), cancellationToken);
            return View(schedules);
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Login", "Auth");
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
            var result = await mediator.Send(new CreateScheduleCommand(User) { ServiceId = id }, cancellationToken);
            TempData["Animals"] = result.Animals!.SerializeObject();
            return View(result);
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (AnimalNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (ServiceNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno. Não foi possível completar seu agendamento!";
        }

        return RedirectToAction("Index", "Service");
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateScheduleCommand command,
        CancellationToken cancellationToken)
    {
        var animalsJson = TempData["Animals"] as string;
        command.Animals = JsonConvert.DeserializeObject<List<AnimalDetailsViewModel>>(animalsJson!);

        if (!ModelState.IsValid)
            return View(command);

        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index", "Service");
        }
        catch (BusyScheduleException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View(command);
        }
        catch (MaximumBookingsPerAnimalExceededException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View(command);
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (ServiceNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index", "Service");
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

            if (await _scheduleService.IsMaximumBookingsExceeded((DateTime)model.Date, (TimeSpan)model.Time!,
                    cancellationToken))
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
    public async Task<IActionResult> Delete([FromForm] DeleteScheduleViewModel model,
        CancellationToken cancellationToken)
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