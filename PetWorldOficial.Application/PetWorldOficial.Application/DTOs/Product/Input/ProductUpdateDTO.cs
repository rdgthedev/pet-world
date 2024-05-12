using Microsoft.AspNetCore.Http;

namespace PetWorldOficial.Application.DTOs.Product.Input;

public class ProductUpdateDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get;  set; } 
    public IFormFile? Image { get; set; } 
    public double? Price { get; set; }
    public int? SupplierId { get; set; }
}