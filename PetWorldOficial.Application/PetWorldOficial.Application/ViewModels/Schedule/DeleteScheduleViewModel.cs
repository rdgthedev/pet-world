using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.Schedule;

public class DeleteScheduleViewModel
{
    [Required] public int AnimalId { get; set; }
    [Required] public int ServiceId { get; set; }
    [Required] public int Id { get; set; }

    [Required(ErrorMessage = "A data é obrigatória!")]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "O horário é obrigatório!")]
    public TimeSpan? Time { get; set; }

    public Domain.Entities.Animal? Animal { get; set; }
    public Domain.Entities.Service? Service { get; set; }
}