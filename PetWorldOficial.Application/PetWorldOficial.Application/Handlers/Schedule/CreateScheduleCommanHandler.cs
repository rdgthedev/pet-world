using System.Reflection.Metadata;
using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Settings;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Application.ViewModels.Schedule;
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
    public async Task<CreateScheduleCommand> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Animals is null)
            {
                var user = await userService.GetByUserNameAsync(request.UserPrincipal?.Identity?.Name!,
                    cancellationToken);

                if (user is null)
                    throw new UserNotFoundException("Ocorreu um erro. Tente fazer o login novamente no site." +
                                                    " Possívelmente sua sessão foi expirada!");

                var service = await serviceService.GetById(request.ServiceId, cancellationToken)
                              ?? throw new ServiceNotFoundException("Ocorreu um erro. Nenhum serviço encontrado!");

                var animals = await animalService.GetByUserId(user.Id, cancellationToken);

                //Gerar lista de horários com intervalos (30 em 30min)

                //Pegar agendamentos por data.

                //Selecionar horários dos agendamentos.

                request.UserId = user.Id;
                request.Animals = animals;
                request.ServiceName = service.Name;
                request.ServicePrice = service.Price;

                return request;
            }

            if (request.Time < openingHours.Value.Open || request.Time > openingHours.Value.Close)
                throw new Exception();

            ERole roleName;

            //Verifica tipo de serviço e define o perfil do funcionário (Higiene ou Veterinário).
            if (request.ServiceName == "Banho"
                || request.ServiceName == "Tosa"
                || request.ServiceName == "Banho & Tosa")
            {
                roleName = ERole.Grooming;
            }
            else
                roleName = ERole.Veterinary;

            List<ScheduleDetailsViewModel?> schedules = null!;

            //Conta o número de funcionários de um determinado perfil.
            var usersIds = (await userRepository.GetUsersByRoleAsync(roleName, cancellationToken))
                .ToList()
                .Select(u => u!.Id)
                .ToList()
                .Count;

            //Conta o número de agendamentos filtrando por data e horário.
            

            //!Verifica disponibilidade para agendamento (agendamentos < funcionários)
                //throw new FalhaNoAgendamento


            //Realiza o agendamento.


            //Envia email ou SMS.
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}