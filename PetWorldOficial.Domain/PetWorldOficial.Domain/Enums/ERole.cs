using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Domain.Enums;

public enum ERole
{
    [Display(Name = "Administrador")]
    Admin = 1,
    [Display(Name = "Cliente")]
    User = 2,
    [Display(Name = "Higienização de pets")]
    Grooming = 3,
    [Display(Name = "Veterinário")]
    Veterinary = 4
}