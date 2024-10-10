using Microsoft.AspNetCore.Mvc.Formatters;
using PetWorldOficial.Application.DTO;

namespace PetWorldOficial.Application.Utils;

public static class ConsecutiveTimes
{
    public static List<TimeDTO> Get(
        List<TimeDTO> timesDtos,
        int defaultRange)
    {
        var adjustedTimes = timesDtos.OrderBy(t => t.Time).ToList();

        foreach (var timeDto in timesDtos)
        {
            var indexAHead = timesDtos.IndexOf(timeDto) + 1;
            var timeAHead = timesDtos[indexAHead];

            if (!IsConsecutive(timeDto.Time, timeAHead.Time, defaultRange)
                || IsOccupied(timeDto)
                || IsOccupied(timeAHead))
                timeDto.Status = false;
        }

        return timesDtos;
    }

    private static bool IsConsecutive(TimeSpan currentTime, TimeSpan timeAHead, int defaultRange)
    {
        var differenceTimes = Math.Abs(currentTime.Subtract(timeAHead).TotalMinutes);
        return differenceTimes.Equals(defaultRange);
    }

    private static bool IsOccupied(TimeDTO time)
        => time.Status.Equals(false);
}