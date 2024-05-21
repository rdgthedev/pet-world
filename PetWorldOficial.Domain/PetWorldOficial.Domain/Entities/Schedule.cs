﻿using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Schedule : Entity
{
    public int AnimalId { get; private set; }
    public int ServiceId { get; private set; }
    public DateTime Date { get; private set; }
    public string Observation { get; private set; }

    public Schedule(
        int animalId, 
        int serviceId,
        DateTime date,
        string observation)
    {
        AnimalId = animalId;
        ServiceId = serviceId;
        Date = date;
        Observation = observation;
    }
}