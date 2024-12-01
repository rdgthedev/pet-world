using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Utils;

public static class RoleUtils
{
    public static ERole GetRole(string categoryName)
    {
        var roleDisplayMapping = new Dictionary<string, ERole>()
        {
            { "Administrador", ERole.Admin },
            { "Higienização de Pets", ERole.Grooming },
            { "Cliente", ERole.User },
            { "Veterinário", ERole.Veterinary }
        };
        
        if (roleDisplayMapping.TryGetValue(categoryName, out var role))
            return role;
        
        if (Enum.TryParse(categoryName, true, out role))
            return role;

        throw new Exception("Perfil não encontrado!");
    }
}