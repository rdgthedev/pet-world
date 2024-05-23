using PetWorldOficial.Domain.Entities;

namespace PetworldOficial.MVC.ViewModels.Schedule;

public class ScheduleRegisterViewModel
{
    public Animal? Animal { get; set; }
    public Service Service { get; set; }
    public DateTime Date { get; set; }
    public string Observation { get; set; }
}