using PetWorldOficial.Application.DTOs.Service.Output;

namespace PetworldOficial.MVC.ViewModels.Service;

public class ScheduleViewModel
{
    public IEnumerable<OutputServiceDTO> Services { get; set; }
}