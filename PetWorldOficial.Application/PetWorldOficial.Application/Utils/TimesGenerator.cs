using PetWorldOficial.Application.DTO;

namespace PetWorldOficial.Application.Utils;

public static class TimesGenerator
{
    private static readonly TimeSpan _startTime = new TimeSpan(8, 0, 0);
    private static readonly TimeSpan _finishTime = new TimeSpan(18, 0, 0);
    private static readonly TimeSpan _startLunchTime = new TimeSpan(12, 0, 0);
    private static readonly TimeSpan _finishLunchTime = new TimeSpan(13, 0, 0);

    public static List<TimeDTO> Generate(DateTime schedulingDate, int defaultRange)
    {
        var times = new List<TimeDTO>();

        for (var time = _startTime; time < _finishTime; time = time.Add(TimeSpan.FromMinutes(defaultRange)))
        {
            times.Add(IsValidTime(schedulingDate, time, defaultRange)
                ? new TimeDTO { Time = time, Status = true }
                : new TimeDTO { Time = time, Status = false });
        }

        return times;
    }

    private static bool IsValidTime(DateTime schedulingDate, TimeSpan time, int defaultRange)
    {
        var currentTime = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.Minute, 0);

        var isNotLunchTime = time < _startLunchTime || time >= _finishLunchTime;
        var differenceTimeInMinutes = true;

        if (DateTime.Now.Date == schedulingDate.Date)
            differenceTimeInMinutes = time > currentTime && time.Subtract(currentTime).TotalMinutes >= defaultRange;

        return differenceTimeInMinutes && isNotLunchTime;
    }
}