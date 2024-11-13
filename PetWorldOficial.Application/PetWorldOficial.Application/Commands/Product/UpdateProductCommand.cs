using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Supplier;

namespace PetWorldOficial.Application.Commands.Product;

public record UpdateProductCommand : IRequest<UpdateProductCommand>
{
    [Required(ErrorMessage = "O id é obrigatório!")]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória!")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "O preço não pode ser vazio!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero!")]
    public decimal? Price { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "A categoria é obrigatória!")]
    public int CategoryId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O fornecedor é obrigatório!")]
    public int SupplierId { get; set; }
    
    [Required(ErrorMessage = "A quantidade não pode ser vazia!")]
    [Range(1, int.MaxValue, ErrorMessage = "deve ser inserido no mínimo um produto!")]
    public int? QuantityInStock { get; set; }
    public IFormFile? File { get; set; }

    public string CategoryName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public int ProductId { get; set; }

    public IEnumerable<CategoryDetailsViewModel> Categories { get; set; } = Enumerable.Empty<CategoryDetailsViewModel>();

    public IEnumerable<SupplierDetailsViewModel> Suppliers { get; set; } = Enumerable.Empty<SupplierDetailsViewModel>();
}