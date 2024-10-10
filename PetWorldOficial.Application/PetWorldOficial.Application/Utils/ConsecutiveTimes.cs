using Microsoft.AspNetCore.Mvc.Formatters;

namespace PetWorldOficial.Application.Utils;

public static class ConsecutiveTimes
{
    public static Dictionary<TimeSpan, TimeSpan> Get(
        List<TimeSpan> emptyTimes,
        int defaultRange) 
    {
        var consecutiveEmptyTimes = new Dictionary<TimeSpan, TimeSpan>();

        for (int i = 0; i < emptyTimes.Count - 1; i++)
        {
            if (emptyTimes[i + 1] - emptyTimes[i] == TimeSpan.FromMinutes(defaultRange))
            {
                if (!consecutiveEmptyTimes.ContainsKey(emptyTimes[i]))
                {
                    consecutiveEmptyTimes.Add(emptyTimes[i], emptyTimes[i + 1]);
                }
            }
        }

        return consecutiveEmptyTimes;
    }
}