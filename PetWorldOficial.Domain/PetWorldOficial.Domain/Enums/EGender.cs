using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Domain.Enums;

public enum EGender
{
    [Display(Name = "Masculino")]
    Male = 1,
    
    [Display(Name = "Feminino")]
    Female = 2
}