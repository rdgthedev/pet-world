using System.Reflection.Metadata;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Settings;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;
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

            //Busca serviço por nome
            var serviceVm = await serviceService.GetByName(request.ServiceName, cancellationToken);

            if (serviceVm is null)
                throw new Exception();

            //Verifica tipo de serviço e define o perfil do funcionário (Higiene ou Veterinário).
            ERole roleName;

            if (request.ServiceName == "Banho"
                || request.ServiceName == "Tosa"
                || request.ServiceName == "Banho & Tosa")
            {
                roleName = ERole.Grooming;
            }
            else
                roleName = ERole.Veterinary;


            //Conta o número de funcionários de um determinado perfil.
            var countEmployeesInRole = await userService.CountUsersByRoleAsync(roleName);

            if (countEmployeesInRole == 0)
                throw new Exception("Nenhum funcionário encontrado para esse tipo de serviço!");


            //Conta o número de agendamentos filtrando por data e horário.
            var countSchedulesByDateAndTime = (await scheduleService.GetAll(cancellationToken))
                .Where(s => s.Date == request.Date
                && s.Time > request.Time!.Value
                && request.Time.Value.Add(TimeSpan.FromMinutes(serviceVm.DurationInMinutes)) <= s.Time)
                .Count();

            //!Verifica disponibilidade para agendamento (agendamentos >= funcionários)
            //throw new FalhaNoAgendamento

            int standardTime = 30;

            var timesServiceFits = serviceVm.DurationInMinutes / standardTime;

            if (countSchedulesByDateAndTime >= countEmployeesInRole
                || countSchedulesByDateAndTime < timesServiceFits)
                throw new Exception("Não há horário disponível!");

            if (timesServiceFits >= 2)
            {
                //Segrega o serviço em dois.
                //Realiza o agendamento
                //Envia e-mail ou SMS.
            }

            //Realiza agendamento.
            //Envia email ou SMS.

            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}