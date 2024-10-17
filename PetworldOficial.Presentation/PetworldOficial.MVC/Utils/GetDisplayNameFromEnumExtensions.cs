using System.ComponentModel.DataAnnotations;
using PetWorldOficial.Domain.Enums;

namespace PetworldOficial.MVC.Utils;

public static class GetDisplayNameFromEnumExtensions
{
    public static string GetDisplayName(this Enum @enum)
    {
        return @enum.GetType()
                .GetField(@enum.ToString())!
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault()
            is DisplayAttribute displayAttribute
            ? displayAttribute.Name!
            : @enum.ToString();
    }
}