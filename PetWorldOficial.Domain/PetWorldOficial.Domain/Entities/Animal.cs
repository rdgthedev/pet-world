using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Animal : Entity
{
    protected Animal() { }

    public Animal(
        string name,
        EGender gender,
        string race,
        int categoryId,
        int ownerId,
        string? imageUrl
    )
    {
        Name = name;
        Gender = gender;
        Race = race;
        CategoryId = categoryId;
        OwnerId = ownerId;
        ImageUrl = imageUrl;
        CreatedAt = DateTime.Now;
        Schedullings = new();
    }
    
    public string Name { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public EGender Gender { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public string? ImageUrl { get; set; }
    public string Race { get; private set; }
    // public Race Race { get; private set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int OwnerId { get; private set; }
    public User Owner { get; private set; }
    public List<Schedulling> Schedullings { get; private set; }
}