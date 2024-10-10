using PetWorldOficial.Application.DTO;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Utils;

public static class Validation
{
    public static bool IsNeedOne(int number)
        => number.Equals(1);

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