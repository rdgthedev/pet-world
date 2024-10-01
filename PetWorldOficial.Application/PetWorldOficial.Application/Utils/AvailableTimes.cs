using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Utils;

public static class AvailableTimes
{
    public static List<TimeSpan> Generate(int interval)
    {
        var times = new List<TimeSpan>();

        var startTime = new TimeSpan(8, 0, 0);
        var finishTime = new TimeSpan(18, 0, 0);

        for (var time = startTime; time <= finishTime; time = time.Add(TimeSpan.FromMinutes(interval)))
            times.Add(time);

        return times;
    }
}