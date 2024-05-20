using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.DTOs.Product;

public class DeleteProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 
    public string Image { get; set; } 
    public double Price { get; set; }
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    public DeleteProductDTO(){ }
    
    public DeleteProductDTO(int id, 
        string name, 
        string description, 
        string image, 
        double price, 
        int supplierId, 
        Supplier? supplier)
    {
        Id = id;
        Name = name;
        Description = description;
        Image = image;
        Price = price;
        SupplierId = supplierId;
        Supplier = supplier;
    }
}