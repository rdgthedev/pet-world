using PetWorldOficial.Application.DTOs.Service.Output;

namespace PetworldOficial.MVC.ViewModels.Schedule;

public class ScheduleServicesViewModel
{
    public IEnumerable<OutputServiceDTO> Services { get; set; }
}