namespace PetWorldOficial.Application.ViewModels.Product;

public record ProductDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int SupplierId { get; set; }
    public Domain.Entities.Supplier Supplier { get; set; } = null!;
}