using System.ComponentModel.DataAnnotations;
using PetWorldOficial.Domain.Entities;

namespace PetworldOficial.MVC.Models.Schedule;

public class RegisterScheduleViewModel
{
    [Required]
    public int AnimalId { get; set; }     
    
    [Required]
    public int ServiceId { get; set; } 
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public TimeSpan Time { get; set; }
    public string? Observation { get; set; }

    public Service? Service { get; set; }
    public IEnumerable<Animal?> Animals { get; set; } = new List<Animal>();
}