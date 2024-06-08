namespace PetWorldOficial.Application.DTOs.Schedule.Output;

public class OutputScheduleDTO
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public int ServiceId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Observation { get; set; }
    
    public Domain.Entities.Service Service { get; set; }
    public Domain.Entities.Animal Animal { get; set; }
}