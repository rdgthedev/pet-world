using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Category : Entity
{
    public Category()
    {
    }

    public Category(
        string title,
        string type)
    {
        Title = title;
        Type = type;
        CreatedAt = DateTime.Now;
    }

    public string Title { get; private set; }
    public string Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public List<Product> Products { get; private set; }
    public List<Animal> Animals { get; private set; }
    public List<Service> Services { get; private set; }
    // public List<Schedulling> Schedullings { get; private set; }
}