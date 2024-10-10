using PetWorldOficial.Application.DTO;

namespace PetWorldOficial.Application.Utils;

public static class TimesGenerator
{
    private static readonly TimeSpan _startTime = new TimeSpan(8, 0, 0);
    private static readonly TimeSpan _finishTime = new TimeSpan(18, 0, 0);
    private static readonly TimeSpan _startLunchTime = new TimeSpan(12, 0, 0);
    private static readonly TimeSpan _finishLunchTime = new TimeSpan(13, 0, 0);

    public static List<TimeDTO> Generate(int defaultRange)
    {
        var times = new List<TimeDTO>();

        for (var time = _startTime; time < _finishTime; time = time.Add(TimeSpan.FromMinutes(defaultRange)))
        {
            if (!IsValidTime(DateTime.Now.TimeOfDay, time))
                times.Add(new TimeDTO(time, false));

            times.Add(new TimeDTO(time, true));
        }

        return times;
    }

    private static bool IsValidTime(TimeSpan timeOfDay, TimeSpan time)
    {
        var isNotLunchTime = time >= _startLunchTime && time <= _finishLunchTime;
        var differenceTimeInMinutes = Math.Abs(time.Subtract(timeOfDay).TotalMinutes) >= 30;

        return differenceTimeInMinutes && isNotLunchTime;
    }
}