namespace PetWorldOficial.Application.DTOs.Schedule;

public class DeleteScheduleDTO
{
    public string AnimalName { get; set; }
    public string ServiceName { get; set; }
    public int AnimalId { get; set; }
    public int ServiceId { get; set; }
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
}