using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.DTOs.Product;

public class UpdateProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get;  set; }
    public string? ImageUrl { get; set; } 
    public double Price { get; set; } 
    public int SupplierId { get; set; }

    public UpdateProductDTO(
        int id, 
        string name, 
        string description, 
        string? imageUrl, 
        double price, 
        int supplierId)
    {
        Id = id;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Price = price;
        SupplierId = supplierId;
    }
}