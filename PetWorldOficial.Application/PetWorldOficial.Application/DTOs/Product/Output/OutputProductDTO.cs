using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.DTOs.Product.Output;

public class OutputProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; } 
    public string Image { get; set; } 
    public double Price { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}