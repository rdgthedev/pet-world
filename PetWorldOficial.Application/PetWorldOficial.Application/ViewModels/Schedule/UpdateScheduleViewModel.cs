using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.Schedule;

public class UpdateScheduleViewModel
{
    [Required]
    public int Id { get; set; }

    [Required] 
    public int AnimalId { get; set; }

    [Required] 
    public int ServiceId { get; set; }


    [Required(ErrorMessage = "A data é obrigatória!")]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "O horário é obrigatório!")]
    [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan? Time { get; set; }

    public string? Observation { get; set; }

    public Domain.Entities.Animal? Animal { get; set; }
    public Domain.Entities.Service? Service { get; set; }
}