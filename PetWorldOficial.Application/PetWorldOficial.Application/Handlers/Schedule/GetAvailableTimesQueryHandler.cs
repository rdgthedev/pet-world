using MediatR;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class GetAvailableTimesQueryHandler(
    IScheduleService scheduleService,
    IUserService userService) : IRequestHandler<GetAvailableTimesQuery, IEnumerable<TimeSpan>>
{
    private const int _defaultRange = 30;

    public async Task<IEnumerable<TimeSpan>> Handle(GetAvailableTimesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var roleName = RoleUtils.GetRoleByServiceName(request.ServiceName);
            
            if (roleName.ToString() is null)
                throw new Exception("Ocorreu um erro. Perfil não encontrado!");

            var employeesIds = (await userService.GetUsersByRoleAsync(roleName, cancellationToken)).Select(u => u.Id);
            
            if (employeesIds is null || !employeesIds.Any())
                throw new UserNotFoundException("Nenhum funcionário com esse perfil foi encontrado!");

            var total = request.DurationInMinutes / _defaultRange;
            var availableTimes = AvailableTimes.Generate();

            var scheduledTimesByDate =
                await scheduleService.GetAllSchedulesTimesByDate(request.Date, employeesIds, cancellationToken);

            if (!scheduledTimesByDate.Any())
                return availableTimes;
            
            var times = availableTimes.Where(t =>
                !scheduledTimesByDate.Any(st =>
                    st >= t 
                    && st < t.Add(TimeSpan.FromMinutes(request.DurationInMinutes))));

            if (!Validation.IsNeedOne(total))
                times = ConsecutiveTimes.Get(times.ToList(), _defaultRange);

            return times;
        }
        catch (Exception)
        {
            throw;
        }
    }
}