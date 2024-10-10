using System.Net.NetworkInformation;
using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.DTO;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class GetAvailableTimesQueryHandler(
    IOptions<Settings.Settings> options,
    IUserService userService,
    IScheduleService scheduleService)
    : IRequestHandler<GetAvailableTimesQuery, List<TimeDTO>>
{
    private readonly int _defaultRange = options.Value.Range.DefaultRangeServices;

    public async Task<List<TimeDTO>> Handle(
        GetAvailableTimesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var times = TimesGenerator.Generate(_defaultRange);
            var roleName = RoleUtils.GetRoleByServiceName(request.ServiceName);

            var employeesCount = await userService.CountUsersByRoleAsync(roleName);

            if (employeesCount > 0)
                throw new UserNotFoundException("Não é possível agendar este serviço. Tente novamente mais tarde!");

            var schedulingsTimes = await scheduleService.GetAllSchedulesTimesByDate(request.Date, cancellationToken);

            if (!schedulingsTimes.Any())
                return times;

            var timesInUse = schedulingsTimes
                .GroupBy(t => t)
                .Select(g => new TimeDTO(g.Key, g.Count() < employeesCount))
                .ToList();

            var adjustedTimes = AdjustTimeStatus(times, timesInUse);

            if (!IsDurationOneHour(request.DurationInMinutes))
                return adjustedTimes;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<TimeDTO> AdjustTimeStatus(List<TimeDTO> times, List<TimeDTO> timesInUse)
    {
        var adjustedTimes = times.Select(time =>
        {
            var matchingTimeInUse = timesInUse.FirstOrDefault(t => t.Time == time.Time);

            if (matchingTimeInUse != null && time.Status != matchingTimeInUse.Status)
                time.Status = matchingTimeInUse.Status;

            return time;
        }).ToList();

        return adjustedTimes;
    }

    private bool IsDurationOneHour(int durationInMinutes)
        => durationInMinutes == 60;
}