using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Supplier;

namespace PetWorldOficial.Application.Commands.Product;

public record CreateProductCommand : IRequest<CreateProductCommand>
{
    [Required(ErrorMessage = "O Nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Descrição é obrigatória!")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "A imagem é obrigatória!")]
    public IFormFile File { get; set; } = null!;

    [Range(0.01, double.MaxValue, ErrorMessage = "O Preço deve ser maior que zero!")]
    public decimal? Price { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "A Categoria é obrigatória!")]
    public int CategoryId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O Fornecedor é obrigatório!")]
    public int SupplierId { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Deve ser inserido no mínimo um produto!")]
    public int? Quantity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public int ProductId { get; set; }

    public IEnumerable<CategoryDetailsViewModel> Categories { get; set; } = Enumerable.Empty<CategoryDetailsViewModel>();

    public IEnumerable<SupplierDetailsViewModel> Suppliers { get; set; } = Enumerable.Empty<SupplierDetailsViewModel>();
}