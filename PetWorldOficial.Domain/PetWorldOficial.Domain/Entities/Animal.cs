using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Animal : Entity
{
    public string Name { get; private set; }
    public string Species { get; private set; }
    public string Race { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public EGender Gender { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    public List<Schedule> Schedules { get; private set; }
    
    public Animal(
        string name, 
        string species, 
        string race, 
        DateTime? birthDate, 
        EGender gender, 
        int userId)
    {
        Name = name;
        Species = species;
        Race = race;
        BirthDate = birthDate;
        Gender = gender;
        UserId = userId;
    }
}