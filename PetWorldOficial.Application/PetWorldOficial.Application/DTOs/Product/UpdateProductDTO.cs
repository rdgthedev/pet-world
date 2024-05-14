using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.DTOs.Product;

public class UpdateProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get;  set; } = string.Empty;
    public string? ImageUrl { get; set; } 
    public double Price { get; set; } 
    public int SupplierId { get; set; }
    public IEnumerable<Supplier>? Suppliers { get; set; }
}