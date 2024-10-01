using System.Reflection.Metadata;
using MediatR;
using MediatR.Pipeline;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Settings;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class CreateScheduleCommanHandler(
    IScheduleService scheduleService,
    IUserService userService,
    IAnimalService animalService,
    IServiceService serviceService,
    IUserRepository userRepository,
    IOptions<OpeningHours> openingHours) : IRequestHandler<CreateScheduleCommand, CreateScheduleCommand>
{
    private const int _defaultRange = 30;

    public async Task<CreateScheduleCommand> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Animals is null || !request.Animals.Any())
            {
                var user = await userService.GetByUserNameAsync(request.UserPrincipal?.Identity?.Name!,
                    cancellationToken);
                if (user is null)
                    throw new UserNotFoundException("Ocorreu um erro! Por favor tente se reconectar.");

                request.Animals = await animalService.GetByUserIdWithOwnerAndRace(user.Id, cancellationToken);
                if (request.Animals is null || !request.Animals.Any())
                    throw new UserNotFoundException("Nenhum pet encontrado!");

                var service = await serviceService.GetById(request.ServiceId, cancellationToken);
                if (service is null)
                    throw new ServiceNotFoundException("Serviço não encontrado!");

                request.ServiceName = service.Name;
                request.ServicePrice = service.Price;
                request.DurationInMinutes = service.DurationInMinutes;

                return request;
            }

            var roleName = RoleUtils.GetRoleByServiceName(request.ServiceName);

            if (roleName.ToString() is null)
                throw new Exception("Ocorreu um erro. Não foi possível realizar o seu agendamento!");

            var employeesIds = (await userService.GetUsersByRoleAsync(roleName, cancellationToken)).Select(u => u.Id);
            if (!employeesIds.Any())
                throw new UserNotFoundException("Nenhum funcionário cadastrado com este perfil!");

            var schedules = await scheduleService.GetAll(cancellationToken);

            var conflicts = schedules.Where(s =>
                s.Date.Date == request.Date!.Value.Date
                && s.Time < request.Time!.Value.Add(TimeSpan.FromMinutes(request.DurationInMinutes))
                && s.Time.Add(TimeSpan.FromMinutes(s.Service.DurationInMinutes)) > request.Time.Value);

            if (conflicts.Any())
                throw new Exception("Conflito de horário! O horário desejado já está agendado.");

            var availableEmployeeId =
                employeesIds.FirstOrDefault(e => !conflicts.Select(c => c.Employee.Id).Contains(e));

            if (request.EmployeeId == 0)
                throw new UserNotFoundException("Nenhum funcionário disponível!");

            await scheduleService.Create(request, cancellationToken);
            request.Message = $"{request.ServiceName} agendado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}