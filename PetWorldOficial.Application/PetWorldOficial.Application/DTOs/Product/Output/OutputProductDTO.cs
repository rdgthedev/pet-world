using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.DTOs.Product.Output;

public class OutputProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 
    public string ImageUrl { get; set; } 
    public double Price { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}