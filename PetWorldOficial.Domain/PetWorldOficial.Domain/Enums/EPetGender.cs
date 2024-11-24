using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Domain.Enums;

public enum EPetGender
{
    [Display(Name = "Macho")]
    Male = 1,
    
    [Display(Name = "Fêmea")]
    Female = 2
}