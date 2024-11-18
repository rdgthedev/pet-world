using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Domain.Enums;

public enum ESchedullingStatus
{
    [Display(Name = "Pendente")]
    Pending = 0,
    [Display(Name = "Cancelado")]
    Canceled = 1,
    [Display(Name = "Finalizado")]
    Finished = 2
}