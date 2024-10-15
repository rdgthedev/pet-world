using PetWorldOficial.Application.DTO;

namespace PetWorldOficial.Application.Utils;

public static class Consecutive
{
    public static List<TimeDTO> GetTimes(
        List<TimeDTO> timesDtos,
        int defaultRange)
    {
        var adjustedTimes = timesDtos.OrderBy(t => t.Time).ToList();

        foreach (var timeDto in timesDtos)
        {
            var indexAHead = timesDtos.IndexOf(timeDto) + 1;

            if (indexAHead > timesDtos.Count - 1)
            {
                timesDtos.Remove(timeDto);
                break;
            }

            var timeAHead = timesDtos[indexAHead];

            if (!Validation.IsConsecutive(timeDto.Time, timeAHead.Time, defaultRange)
                || Validation.IsOccupied(timeDto)
                || Validation.IsOccupied(timeAHead))
            {
                if (timeDto.Time != new TimeSpan(8, 0, 0))
                {
                    var timeBefore = timesDtos[timesDtos.IndexOf(timeDto) - 1];
                    timeBefore.Status = false;
                }

                timeDto.Status = false;
            }
        }

        return timesDtos;
    }
}