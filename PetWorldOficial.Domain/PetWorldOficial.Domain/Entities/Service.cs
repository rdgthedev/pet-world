using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Service : Entity
{
    protected Service()
    {
    }

    public Service(
        string name,
        double price,
        string imageUrl,
        int categoryId,
        int durationInMinutes)
    {
        Name = name;
        Price = price;
        ImageUrl = imageUrl;
        CategoryId = categoryId;
        DurationInMinutes = durationInMinutes;
        CreatedAt = DateTime.Now;
        Status = EActivingStatus.Active;
        Schedullings = new();
    }

    public string Name { get; private set; }
    public string ImageUrl { get; private set; }
    public int DurationInMinutes { get; private set; }
    public double Price { get; private set; }
    public EActivingStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public List<Schedulling> Schedullings { get; private set; }
}