﻿using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Scheduling;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Scheduling;

public class CreateScheduleCommanHandler(
    IScheduleService scheduleService,
    IUserService userService,
    IAnimalService animalService,
    IServiceService serviceService,
    IOptions<Settings.Settings> options) : IRequestHandler<CreateScheduleCommand, CreateScheduleCommand>
{
    private readonly int _defaultRange = options.Value.Range.DefaultRangeServices;

    public async Task<CreateScheduleCommand> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.AnimalId is null or 0)
            {
                var email = request.UserPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                var user = await userService.GetByEmailAsync(
                    email!,
                    cancellationToken);

                if (user is null)
                    throw new UserNotFoundException("Ocorreu um erro! Por favor tente se reconectar.");

                request.Animals = request.UserPrincipal!.IsInRole(ERole.User.ToString())
                    ? await animalService.GetByUserIdWithOwnerAndRace(user.Id, cancellationToken)
                    : await animalService.GetWithOwnerAndRace(cancellationToken);

                var service = await serviceService.GetById(request.ServiceId, cancellationToken);

                if (service is null)
                    throw new ServiceNotFoundException("Serviço não encontrado!");

                request.ServiceName = service.Name;
                request.ServicePrice = service.Price;
                request.DurationInMinutes = service.DurationInMinutes;
                request.CategoryName = service.Category.Title;
                request.UserId = user.Id;

                return request;
            }

            var animalScheduling = await scheduleService.GetByAnimalIdAndDateAndTime(
                request.AnimalId!.Value,
                request.Date!.Value,
                request.Time!.Value,
                cancellationToken);

            if (animalScheduling is not null)
                throw new SchedulingAlreadyExistsException("Não foi possível realizar o seu agendamento." +
                                                           " Já existe um agendamento nesta data e horário para este pet.");

            if (Validation.IsDurationOneHour(request.DurationInMinutes))
            {
                animalScheduling = await scheduleService.GetByAnimalIdAndDateAndTime(
                    request.AnimalId!.Value,
                    request.Date!.Value,
                    request.Time!.Value.Add(TimeSpan.FromMinutes(_defaultRange)),
                    cancellationToken);

                if (animalScheduling != null)
                    throw new SchedulingAlreadyExistsException("Não foi possível realizar o seu agendamento." +
                                                               "Você não pode agendar nesse horário, pois há um serviço para o seu pet no horário a frente agendado.");
            }

            var schedulings = AddSchedullings(request);

            if (schedulings == null)
                throw new Exception("Não foi possível realizar o seu agendamento. Tente novamente mais tarde!");

            await scheduleService.CreateInBatch(schedulings, cancellationToken);
            request.Message = "Agendamento realizado com sucesso!";

            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<CreateScheduleCommand> AddSchedullings(CreateScheduleCommand request)
    {
        var schedulings = new List<CreateScheduleCommand>();

        if (!Validation.IsDurationOneHour(request.DurationInMinutes))
            schedulings.Add(request);
        else
        {
            schedulings.Add(request);

            var newRequest = new CreateScheduleCommand(request.UserPrincipal)
            {
                AnimalId = request.AnimalId,
                ServiceId = request.ServiceId,
                EmployeeId = request.EmployeeId,
                Date = request.Date,
                Time = request.Time!.Value.Add(TimeSpan.FromMinutes(_defaultRange)),
                Observation = request.Observation,
                Code = request.Code
            };

            schedulings.Add(newRequest);
        }

        return schedulings;
    }
}