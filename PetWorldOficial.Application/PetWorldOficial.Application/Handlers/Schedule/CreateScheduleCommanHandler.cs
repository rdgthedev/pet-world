using System.Reflection.Metadata;
using MediatR;
using MediatR.Pipeline;
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
    private const int _defaultRange = 30;

    public async Task<CreateScheduleCommand> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Verificação de pets e serviço, já existente no seu código
            if (request.Animals is null || !request.Animals.Any())
            {
                var user = await userService.GetByUserNameAsync(request.UserPrincipal?.Identity?.Name!, cancellationToken);
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

            //var roleName = RoleUtils.GetRoleByServiceName(request.ServiceName);

            //if (string.IsNullOrEmpty(roleName.ToString()))
            //    throw new Exception("Ocorreu um erro! Perfil não existe.");

            //var totalBlocks = request.DurationInMinutes / _defaultRange;

            //var schedullings = await scheduleService.GetAll(cancellationToken);

            //var scheduledByDateAndTime = schedullings.Where(s => s.Date.Date == request.Date!.Value.Date
            //&& s.Time == request.Time!.Value);

            //if (scheduledByDateAndTime.Any())
            //    throw new Exception("");

            //if (totalBlocks > 1)
            //{

            //}
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}