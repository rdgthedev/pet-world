﻿using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Schedulling : Entity
{
    public Schedulling()
    {
    }

    public Schedulling(
        int animalId,
        int serviceId,
        int employeeId,
        DateTime date,
        TimeSpan time,
        string? observation) : this()
    {
        AnimalId = animalId;
        ServiceId = serviceId;
        EmployeeId = employeeId;
        Date = date;
        Time = time;
        Observation = observation;
        Status = ESchedullingStatus.Created;
        CreatedAt = DateTime.Now;
        Code = Guid.NewGuid();
    }

    public int AnimalId { get; private set; }
    public Animal Animal { get; private set; } = null!;
    public int ServiceId { get; private set; }
    public Service Service { get; private set; } = null!;
    public int EmployeeId { get; private set; }
    public User Employee { get; private set; } = null!;
    public Guid Code { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan Time { get; private set; }
    public string? Observation { get; private set; }
    public ESchedullingStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; set; }
}