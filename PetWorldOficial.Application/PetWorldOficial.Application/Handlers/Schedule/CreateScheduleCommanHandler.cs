using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Settings;
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
                request.CategoryName = service.Category.Title;

                return request;
            }
            
            //Criar lógica para criação de agendamento.
            
            request.Message = $"{request.ServiceName} agendado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}