using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.ViewModels.Schedule;

public class ScheduleDatailsViewModel
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public int ServiceId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string? Observation { get; set; }
    public Service Service { get; set; }
    public Domain.Entities.Animal Animal { get; set; }
}