using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.DTOs.Product;
public class RegisterProductDTO
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get;  set; }
    // public IFormFile Image { get; set; }
    public string? ImageUrl { get; set; }
    public double Price { get; set; }
    public int SupplierId { get; set; }
    
    public IEnumerable<Supplier>? Suppliers { get; set; }
}