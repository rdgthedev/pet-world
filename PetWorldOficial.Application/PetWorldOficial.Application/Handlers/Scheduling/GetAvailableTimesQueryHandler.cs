using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.DTO;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Scheduling;

public class GetAvailableTimesQueryHandler(
    IOptions<Settings.Settings> options,
    IUserService userService,
    IScheduleService scheduleService,
    IServiceService serviceService)
    : IRequestHandler<GetAvailableTimesQuery, List<TimeDTO>>
{
    private readonly int _defaultRange = options.Value.Range.DefaultRangeServices;

    public async Task<List<TimeDTO>> Handle(
        GetAvailableTimesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var service = await serviceService.GetByName(request.ServiceName, cancellationToken);

            if (service is null)
                throw new ServiceNotFoundException(
                    "Não foi possível agendar este serviço no momento. Tente novamente mais tarde!");

            var role = RoleUtils.GetRole(service.Category.Title);
            var times = TimesGenerator.Generate(request.Date, _defaultRange);
            var employeesCount = await userService.CountUsersByRoleAsync(role);

            if (employeesCount <= 0)
                throw new UserNotFoundException(
                    "Não foi possível agendar este serviço no momento. Tente novamente mais tarde!");

            var schedulingsTimes = await scheduleService
                .GetAllSchedulesTimesByDateAndCategory(
                    request.Date,
                    service.Category.Title,
                    cancellationToken);
 
            if (!schedulingsTimes.Any() && !Validation.IsDurationOneHour(request.DurationInMinutes))
                return times;

            var schedulingTimes = schedulingsTimes
                .GroupBy(t => t)
                .Select(g => new TimeDTO { Time = g.Key, Status = g.Count() < employeesCount })
                .ToList();

            var adjustedTimes = AdjustTimeStatus(times, schedulingTimes);

            if (Validation.IsDurationOneHour(request.DurationInMinutes))
                return Consecutive.GetTimes(adjustedTimes, _defaultRange);

            return adjustedTimes;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<TimeDTO> AdjustTimeStatus(List<TimeDTO> times, List<TimeDTO> schedulingTimes)
    {
        var adjustedTimes = times.Select(time =>
        {
            var matchingTimeInUse = schedulingTimes.FirstOrDefault(t => t.Time == time.Time);

            if (matchingTimeInUse != null && time.Status && time.Status != matchingTimeInUse.Status)
                time.Status = matchingTimeInUse.Status;

            return time;
        }).ToList();

        return adjustedTimes;
    }
}