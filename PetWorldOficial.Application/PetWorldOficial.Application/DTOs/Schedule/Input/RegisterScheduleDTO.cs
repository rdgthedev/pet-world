namespace PetWorldOficial.Application.DTOs.Schedule.Input;

public class RegisterScheduleDTO
{
    public int AnimalId { get; set; }
    public int ServiceId { get; set; }
    public DateTime Date { get; set; } 
    public TimeSpan Time { get; set; } 
    public string? Observation { get; set; }

    public RegisterScheduleDTO(
        int animalId,
        int serviceId,
        DateTime date, 
        TimeSpan time,
        string? observation)
    {
        AnimalId = animalId;
        ServiceId = serviceId;
        Date = date;
        Time = time;
        Observation = observation;
    }
}