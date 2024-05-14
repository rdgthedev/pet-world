using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Service : Entity
{
    public string Name { get; private set; }
    public double Price { get; private set; }
    public string ImageUrl { get; private set; }
    
    public Service(
        string name, 
        double price, 
        string imageUrl)
    {
        Name = name;
        Price = price;
        ImageUrl = imageUrl;
    }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Name) || Name.Length < 3)
            return false;
        
        if (Price <= 0)
            return false;

        if (string.IsNullOrEmpty(ImageUrl))
            return false;

        return true;
    }
}