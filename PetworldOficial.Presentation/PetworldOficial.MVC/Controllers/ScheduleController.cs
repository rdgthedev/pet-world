using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Scheduling;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class ScheduleController(
    IScheduleService _scheduleService,
    IMediator mediator,
    IAnimalService animalService,
    IUserService userService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAvailableTimes(
        GetAvailableTimesQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(query, cancellationToken);
            return Json(result);
        }
        catch (UserNotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return StatusCode(404, new { redirectToUrl = Url.Action("Index", "Service") });
        }
        catch (ServiceNotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return StatusCode(404, new { redirectToUrl = Url.Action("Index", "Service") });
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Não é possível agendar este serviço no momento. Tente novamente mais tarde!";
            return StatusCode(500, new { redirectToUrl = Url.Action("Index", "Service") });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailableEmployees(
        GetAvailaEmployeesToSchedulingQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(query, cancellationToken);
            return Json(result);
        }
        catch (UserNotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return StatusCode(404, new { redirectToUrl = Url.Action("Index", "Service") });
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Não é possível agendar este serviço no momento. Tente novamente mais tarde!";
            return StatusCode(500, new { redirectToUrl = Url.Action("Index", "Service") });
        }
    }

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
            TempData["NotFoundScheduling"] = e.Message;
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
        try
        {
            command.Animals = User.IsInRole(ERole.User.ToString())
                ? await animalService.GetByUserIdWithOwnerAndRace(command.UserId!.Value, cancellationToken)
                : await animalService.GetWithOwnerAndRace(cancellationToken);

            // if(User.IsInRole(ERole.Admin.ToString()))
            //     command.Users = await userService.GetAllUsersExceptCurrentAsync()

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index", "Service");
        }
        catch (SchedulingAlreadyExistsException e)
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
    public async Task<IActionResult> Update(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new UpdateSchedulingCommand(User) { Code = id };
            var result = await mediator.Send(command, cancellationToken);
            return View(result);
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
    public async Task<IActionResult> Update(UpdateSchedulingCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        try
        {
            await mediator.Send(command, cancellationToken);
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

        return View(command);
    }


    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new DeleteSchedulingCommand(id), cancellationToken);
            return View(result);
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
    public async Task<IActionResult> Delete(
        [FromForm] DeleteSchedulingCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Cancel(
        [FromQuery] UpdateStatusToCanceledCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.message;
        }
        catch (ScheduleNotFoundException e)
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
    public async Task<IActionResult> Finish(
        [FromQuery] UpdateStatusFinishedCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.message;
        }
        catch (ScheduleNotFoundException e)
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