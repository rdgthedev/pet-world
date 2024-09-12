using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Animal : Entity
{
    protected Animal() { }

    public Animal(
        string name,
        EGender gender,
        int raceId,
        int categoryId,
        int ownerId
    )
    {
        Name = name;
        Gender = gender;
        RaceId = raceId;
        CategoryId = categoryId;
        OwnerId = ownerId;
        CreatedAt = DateTime.Now;
        Schedullings = new();
    }

    public string Name { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public EGender Gender { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public int RaceId { get; private set; }
    public Race Race { get; private set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int OwnerId { get; private set; }
    public User Owner { get; private set; }
    public List<Schedulling> Schedullings { get; private set; }
}