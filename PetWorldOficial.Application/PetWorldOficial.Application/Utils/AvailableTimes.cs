namespace PetWorldOficial.Application.Utils;

public static class AvailableTimes
{
    public static List<TimeSpan> Generate()
    {
        var times = new List<TimeSpan>();

        var startTime = new TimeSpan(8, 0, 0);
        var finishTime = new TimeSpan(18, 0, 0);
        var interval = TimeSpan.FromMinutes(30);
        
        for (var time = startTime; time <= finishTime; time = time.Add(interval))
        {
            times.Add(time);
        }

        return times;
    }
}