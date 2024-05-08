using System.Data;
using System.Drawing;
using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Product : Entity
{
    public Product(){ }
    
    public Product(
        string name, 
        string description, 
        double price, string image, 
        Supplier supplier)
    {
        Name = name;
        Description = description;
        Price = price;
        Image = image;
        Supplier = supplier;
    }
    
    public string Name { get; private set; }
    public string Description { get; private set; } 
    public string Image { get; private set; } 
    public double Price { get; private set; } 
    public Supplier Supplier { get; private set; }
    
    public void Update(
        string name,
        string description,
        string image,
        double price)
    {
        Name = name;
        Description = description;
        Image = image;
        Price = price;
    }
}