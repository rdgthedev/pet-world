using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Utils;

public static class RoleUtils
{
    public static ERole GetRoleByServiceName(string serviceName)
    {
        return serviceName switch
        {
            "Tosa" => ERole.Grooming,
            "Banho&Tosa" => ERole.Grooming,
            "Banho" => ERole.Grooming,
            _ => ERole.Veterinary
        };
    }
}