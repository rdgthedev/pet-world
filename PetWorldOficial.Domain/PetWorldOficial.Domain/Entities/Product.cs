using PetWorldOficial.Domain.Common;
namespace PetWorldOficial.Domain.Entities;

public class Product : Entity
{
    protected Product(){ }
    
    public Product(
        string name,
        string description, 
        double price, 
        string imageUrl,
        int supplierId)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        SupplierId = supplierId;
    }
    
    public string Name { get; private set; }
    public string Description { get; private set; } 
    public string ImageUrl { get; private set; } 
    public double Price { get; private set; }
    public int SupplierId { get; private set; }
    public Supplier Supplier { get; private set; }
}