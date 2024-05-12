using PetWorldOficial.Domain.Common;
namespace PetWorldOficial.Domain.Entities;

public class Product : Entity
{
    protected Product(){ }
    
    public Product(
        string name, 
        string description, 
        double price, 
        string image,
        int supplierId)
    {
        Name = name;
        Description = description;
        Price = price;
        Image = image;
        // Quantity = quantity;
        SupplierId = supplierId;
    }
    
    public string Name { get; private set; }
    public string Description { get; private set; } 
    public string Image { get; private set; } 
    public double Price { get; private set; }
    public int SupplierId { get; private set; }
    public Supplier Supplier { get; private set; }
    
    public void Update(Product product)
    {
        if(!string.IsNullOrEmpty(product.Name))
            Name = product.Name;
        
        Description = product.Description;
        Image = product.Image;
        Price = product.Price;
        SupplierId = product.SupplierId;
    }
}