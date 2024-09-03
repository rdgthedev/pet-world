using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.ViewModels.Product;

public class UpdateProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get;  set; }
    public string? ImageUrl { get; set; } 
    public double Price { get; set; } 
    public int SupplierId { get; set; }
    
    public IEnumerable<Supplier>? Suppliers { get; set; }

    // public UpdateProductViewModel(
    //     int id, 
    //     string name, 
    //     string description, 
    //     string? imageUrl, 
    //     double price, 
    //     int supplierId, 
    //     IEnumerable<Supplier>? suppliers)
    // {
    //     Id = id;
    //     Name = name;
    //     Description = description;
    //     ImageUrl = imageUrl;
    //     Price = price;
    //     SupplierId = supplierId;
    //     Suppliers = suppliers;
    // }
}