namespace PetWorldOficial.Application.Utils;

public static class ConsecutiveTimes
{
    public static List<TimeSpan> Get(List<TimeSpan> emptyTimes, int defaultRange)
    {
        var consecutiveEmptyTimes = new List<TimeSpan>();

        for (int i = 0; i < emptyTimes.Count - 1; i++)
        {
            if (emptyTimes[i + 1] - emptyTimes[i] == TimeSpan.FromMinutes(defaultRange))
            {
                if (!consecutiveEmptyTimes.Contains(emptyTimes[i]))
                    consecutiveEmptyTimes.Add(emptyTimes[i]);

                // consecutiveEmptyTimes.Add(emptyTimes[i + 1]);
            }
        }

        return consecutiveEmptyTimes;
    }
}