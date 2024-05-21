using PetWorldOficial.Domain.Entities;

namespace PetworldOficial.MVC.ViewModels.Schedule;

public class ScheduleRegisterViewModel
{
    public int AnimalId { get; set; }
    public Service Service { get; set; }
    public DateTime ScheduleDate { get; set; }
    public string Observation { get; set; }
}