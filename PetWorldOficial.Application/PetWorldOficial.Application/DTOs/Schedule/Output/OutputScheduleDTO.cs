namespace PetWorldOficial.Application.DTOs.Schedule.Output;

public class OutputScheduleDTO
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public int ServiceId { get; set; }
    public DateTime Date { get; set; }
    public string Observation { get; set; }

}