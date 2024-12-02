using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Scheduling;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Scheduling;

public class UpdateSchedulingCommandHandler(
    IScheduleService scheduleService,
    IScheduleService schedulingService,
    IMapper mapper,
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
                var schedulings = await schedulingService.GetAllByCode(request.Code, cancellationToken);

                if (!schedulings.Any())
                    throw new ScheduleNotFoundException("Ocorreu um erro! Agendamento não encontrado!");

                var scheduling = schedulings.First();

                if (schedulings.Count() > 1)
                    request.SecondId = schedulings.Last().Id;

                request.Id = scheduling.Id;
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
                request.Observation = scheduling.Observation;

                return request;
            }

            var schedulingsToUpdate = UpdateSchedulings(request);
            await scheduleService.UpdateRange(schedulingsToUpdate, cancellationToken);
            
            request.Message = "Agendamento atualizado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<UpdateSchedulingCommand> UpdateSchedulings(
        UpdateSchedulingCommand request)
    {
        var schedulings = new List<UpdateSchedulingCommand>();

        if (!Validation.IsDurationOneHour(request.DurationInMinutes))
        {
            var scheduling = new UpdateSchedulingCommand(request.UserPrincipal)
            {
                Id = request.Id,
                AnimalId = request.AnimalId,
                ServiceId = request.ServiceId,
                EmployeeId = request.EmployeeId,
                Date = request.Date,
                Time = request.Time!.Value.Add(TimeSpan.FromMinutes(_defaultRange)),
                Observation = request.Observation
            };

            schedulings.Add(request);
        }
        else
        {
            schedulings.Add(request);

            var newRequest = new UpdateSchedulingCommand(request.UserPrincipal)
            {
                Id = request.SecondId!.Value,
                AnimalId = request.AnimalId,
                ServiceId = request.ServiceId,
                EmployeeId = request.EmployeeId,
                Date = request.Date,
                Code = request.Code,
                Time = request.Time!.Value.Add(TimeSpan.FromMinutes(_defaultRange)),
                Observation = request.Observation
            };

            schedulings.Add(newRequest);
        }
        
        return schedulings;
    }
}