using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Schedule() : Entity
{
    public Schedule(
        int animalId,
        int serviceId,
        DateTime date,
        TimeSpan time,
        string? observation) : this()
    {
        AnimalId = animalId;
        ServiceId = serviceId;
        Date = date;
        Time = time;
        Observation = observation;
    }

    public int AnimalId { get; private set; }
    public Animal Animal { get; private set; } = null!;
    public int ServiceId { get; private set; }
    public Service Service { get; private set; } = null!;
    public DateTime Date { get; private set; }
    public TimeSpan Time { get; private set; }
    public string? Observation { get; private set; }
    public int DurationInMinutes { get; set; }
}