using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.Schedule;

public class CreateScheduleViewModel
{
    [Required(ErrorMessage = "O pet é obrigatório!")]
    public int? AnimalId { get; set; }

    [Required] 
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "A Data é obrigatória")]
    [DisplayName("Data")]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "O Horário é obrigatório")]
    [DisplayName("Horário")]
    public TimeSpan? Time { get; set; }

    [DisplayName("Obervação")] 
    public string? Observation { get; set; }

    public Domain.Entities.Service? Service { get; set; }
    public IEnumerable<Domain.Entities.Animal>? Animals { get; set; }
    public int UserId { get; set; }
}