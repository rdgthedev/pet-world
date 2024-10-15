using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class UpdateSchedulingCommandHandler(
    IUserService userService,
    IMemoryCache memoryCache,
    IAnimalService animalService,
    IServiceService serviceService,
    IScheduleService schedulingService) : IRequestHandler<UpdateSchedulingCommand, UpdateSchedulingCommand>
{
    public async Task<UpdateSchedulingCommand> Handle(
        UpdateSchedulingCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var scheduling = await schedulingService.GetById(request.SchedulingId, cancellationToken);

            if (scheduling == null)
                throw new ScheduleNotFoundException("Ocorreu um erro! Agendamento não encontrado!");

            request.ServiceName = scheduling.Service.Name;
            request.EmployeeId = scheduling.Employee.Id;
            request.ServicePrice = scheduling.Service.Price;
            request.DurationInMinutes = scheduling.Service.DurationInMinutes;
            request.EmployeeName = scheduling.Employee.Name;
            request.AnimalName = scheduling.Animal.Name;

            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}