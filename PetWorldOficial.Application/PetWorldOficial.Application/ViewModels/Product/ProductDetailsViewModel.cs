using System.Data.Common;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.ViewModels.Product;

public record ProductDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double Price { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
}