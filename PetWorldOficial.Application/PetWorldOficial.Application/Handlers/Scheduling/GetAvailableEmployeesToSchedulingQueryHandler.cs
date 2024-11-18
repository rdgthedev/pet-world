using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Scheduling;

public class GetAvailableEmployeesToSchedulingQueryHandler(
    IUserService userService,
    IScheduleService scheduleService,
    IOptions<Settings.Settings> options)
    : IRequestHandler<GetAvailaEmployeesToSchedulingQuery, List<(int Id, string Name, bool Status)>>
{
    private readonly int _defaultRange = options.Value.Range.DefaultRangeServices;

    public async Task<List<(int Id, string Name, bool Status)>> Handle(
        GetAvailaEmployeesToSchedulingQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var roleName = RoleUtils.GetRoleByServiceName(request.ServiceName);

            var employees = (await userService.GetUsersByRoleAsync(roleName, cancellationToken))
                .Select(e => (e.Id, e.Name, true))
                .ToList();

            if (!employees.Any())
                throw new UserNotFoundException(
                    "Não é possível agendar este serviço no momento. Tente novamente mais tarde!");

            var schedulings = await scheduleService.GetByCategoryAndDate(
                request.CategoryName,
                request.SchedulingDate,
                cancellationToken);

            var scheduledEmployees = EmployeesUtils.GetInvalidEmployeesToConsecutiveServices(
                schedulings,
                request.SchedulingTime,
                request.DurationInMinutes,
                _defaultRange);

            employees = EmployeesUtils.AdjustEmployees(employees, scheduledEmployees);

            return employees;
        }
        catch (Exception)
        {
            throw;
        }
    }
}