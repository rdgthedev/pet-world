using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class UpdateSchedulingCommandHandler(
    IScheduleService scheduleService,
    IScheduleService schedulingService,
    IOptions<Settings.Settings> options) : IRequestHandler<UpdateSchedulingCommand, UpdateSchedulingCommand>
{
    private readonly int _defaultRange = options.Value.Range.DefaultRangeServices;

    public async Task<UpdateSchedulingCommand> Handle(
        UpdateSchedulingCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request.AnimalId == null)
            {
                var scheduling = await schedulingService.GetById(request.SchedulingId, cancellationToken);

                if (scheduling == null)
                    throw new ScheduleNotFoundException("Ocorreu um erro! Agendamento não encontrado!");

                request.ServiceName = scheduling.Service.Name;
                request.EmployeeId = scheduling.Employee.Id;
                request.ServiceId = scheduling.ServiceId;
                request.ServicePrice = scheduling.Service.Price;
                request.DurationInMinutes = scheduling.Service.DurationInMinutes;
                request.EmployeeName = scheduling.Employee.Name;
                request.AnimalName = scheduling.Animal.Name;
                request.CategoryName = scheduling.Service.Category.Title;
                request.AnimalId = scheduling.AnimalId;
                request.Time = scheduling.Time;

                return request;
            }

            var schedulings = AddSchedulings(request);

            await scheduleService.UpdateRange(schedulings, cancellationToken);
            request.Message = "Agendamento atualizado com sucesso!";

            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<UpdateSchedulingCommand> AddSchedulings(
        UpdateSchedulingCommand request)
    {
        var schedulings = new List<UpdateSchedulingCommand>();

        if (!Validation.IsDurationOneHour(request.DurationInMinutes))
        {
            schedulings.Add(request);
        }
        else
        {
            schedulings.Add(request);

            var newRequest = new UpdateSchedulingCommand(request.UserPrincipal)
            {
                SchedulingId = request.SchedulingId + 1,
                AnimalId = request.AnimalId,
                ServiceId = request.ServiceId,
                EmployeeId = request.EmployeeId,
                Date = request.Date,
                Time = request.Time!.Value.Add(TimeSpan.FromMinutes(_defaultRange)),
                Observation = request.Observation
            };

            schedulings.Add(newRequest);
        }

        return schedulings;
    }
}