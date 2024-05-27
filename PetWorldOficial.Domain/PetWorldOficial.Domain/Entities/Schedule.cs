using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Schedule : Entity
{
    public int AnimalId { get; private set; }
    public Animal Animal { get; private set; }
    public int ServiceId { get; private set; }
    public Service Service { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan Time { get; private set; }
    public string Observation { get; private set; }

    public Schedule(
        int animalId, 
        int serviceId,
        DateTime date,
        TimeSpan time,
        string observation)
    {
        AnimalId = animalId;
        ServiceId = serviceId;
        Date = date;
        Time = time;
        Observation = observation;
    }
}