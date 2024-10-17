using PetWorldOficial.Application.DTO;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Utils;

public static class Validation
{
    public static bool IsDurationOneHour(int durationInMinutes)
        => durationInMinutes == 60;

    public static bool IsConsecutive(TimeSpan currentTime, TimeSpan timeAHead, int defaultRange)
    {
        var differenceTimes = Math.Abs(currentTime.Subtract(timeAHead).TotalMinutes);
        return differenceTimes.Equals(defaultRange);
    }

    public static bool IsOccupied(TimeDTO time)
        => time.Time != new TimeSpan(12, 0, 0) && time.Status.Equals(false);

    public static DefaultRangeAndCategoryDTO GetDefaultRangeAndCategoryType(
        string categoryName,
        int defaultRangeGrooming,
        int defaultRangeVeterinary)
    {
        var categoriesDefaultRange = new Dictionary<ECategoryType, int>
        {
            { ECategoryType.Grooming, defaultRangeGrooming },
            { ECategoryType.Veterinary, defaultRangeVeterinary }
        };

        var category = (ECategoryType)Enum.Parse(typeof(ECategoryType), categoryName);

        return new DefaultRangeAndCategoryDTO(categoriesDefaultRange[category], category);
    }
}