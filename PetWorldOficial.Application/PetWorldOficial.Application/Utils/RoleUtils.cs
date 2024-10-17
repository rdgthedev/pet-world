using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Utils;

public static class RoleUtils
{
    public static ERole GetRoleByServiceName(string serviceName)
    {
        return serviceName switch
        {
            "teste" => ERole.Grooming,
            "Tosa" => ERole.Grooming,
            "Banho/Tosa" => ERole.Grooming,
            _ => ERole.Veterinary
        };
    }
}