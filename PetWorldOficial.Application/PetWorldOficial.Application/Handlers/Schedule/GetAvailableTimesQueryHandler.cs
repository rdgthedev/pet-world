using MediatR;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class GetAvailableTimesQueryHandler(
    IScheduleService scheduleService,
    IUserService userService) : IRequestHandler<GetAvailableTimesQuery, IEnumerable<TimeSpan>>
{
    private const int _defaultRangeToGroomingServices = 30;
    private const int _defaultRangeToVeterinaryServices = 60;

    public async Task<IEnumerable<TimeSpan>> Handle(GetAvailableTimesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var roleName = RoleUtils.GetRoleByServiceName(request.ServiceName);

            if (string.IsNullOrEmpty(roleName.ToString()))
                throw new Exception();

            var employeesIds = (await userService.GetUsersByRoleAsync(roleName, cancellationToken)).Select(u => u.Id);

            if (!employeesIds.Any())
                throw new Exception();

            var timesInUse = (await scheduleService.GetAllSchedulesTimesByDate(request.Date, employeesIds, cancellationToken))
                .GroupBy(t => t)
                .Select(g => new {Time = g.Key, Count = g.Count()})
                .ToList();

            var availableTimes = new List<TimeSpan>();

            if (roleName is ERole.Veterinary)
                availableTimes = AvailableTimes.Generate(_defaultRangeToVeterinaryServices);

            if (roleName is ERole.Grooming)
                availableTimes = AvailableTimes.Generate(_defaultRangeToGroomingServices);

            var times = availableTimes.Where(t => !timesInUse.Any(g => g.Time == t && g.Count >= 3));

            return times;

        }
        catch (Exception)
        {
            throw;
        }
    }
}