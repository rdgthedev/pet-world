using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Utils;

public static class EmployeesUtils
{
    public static List<(int Id, string Name, bool Status)> GetInvalidEmployeesToConsecutiveServices(
        IEnumerable<Schedulling> schedulings,
        TimeSpan schedulingTime,
        int durationInMinutes,
        int defaultRange)
    {
        var scheduledEmployees = new List<(int Id, string Name, bool Status)>();
        var timeAHead = new TimeSpan();

        if (Validation.IsDurationOneHour(durationInMinutes))
            timeAHead = schedulingTime.Add(TimeSpan.FromMinutes(defaultRange));

        scheduledEmployees = schedulings
            .Where(s => s.Time == schedulingTime || s.Time == timeAHead)
            .Select(s => (s.Id, s.Employee.UserName!, false))
            .ToList();

        return scheduledEmployees;
    }

    public static List<(int Id, string Name, bool Status)> AdjustEmployees(
        List<(int Id, string Name, bool Status)> allEmployees,
        List<(int Id, string Name, bool Status)> scheduledEmployees)
    {
        var updatedEmployees = allEmployees
            .Select(ae =>
            {
                var se = scheduledEmployees.FirstOrDefault(se => se.Name == ae.Name);
                return se != default ? (ae.Id, ae.Name, se.Status) : ae;
            })
            .ToList();

        return updatedEmployees;
    }
}