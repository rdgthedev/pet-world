using Microsoft.AspNetCore.Http;

namespace PetWorldOficial.Application.DTOs.Product.Input;
public class ProductRegisterDTO
{
    public string Name { get; set; }
    public string Description { get;  set; }
    public IFormFile Image { get; set; }
    public double Price { get; set; }
    // public int Quantity { get; set; }
    public int SupplierId { get; set; }
}