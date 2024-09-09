namespace PetWorldOficial.Application.Utils;

public static class AvailableTimes
{
    public static List<string> Generate()
    {
        var times = new List<string>();

        var startTime = new TimeSpan(8, 0, 0);
        var finishTime = new TimeSpan(18, 0, 0);
        var interval = TimeSpan.FromMinutes(30);

        for (var time = startTime; time <= finishTime; time.Add(interval))
            times.Add(time.ToString(@"HH\:mm"));

        return times;
    }
}