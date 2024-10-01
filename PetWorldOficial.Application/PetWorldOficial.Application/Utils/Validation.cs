using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Utils;

public static class Validation
{
    public static bool IsNeedOne(int number)
        => number.Equals(1);
}