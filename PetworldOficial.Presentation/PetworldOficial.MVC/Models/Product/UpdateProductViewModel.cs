using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Domain.Entities;

namespace PetworldOficial.MVC.Models.Product;

public class UpdateProductViewModel
{
    public UpdateProductViewModel(
        UpdateProductDTO updateProductDto, 
        IEnumerable<Supplier> suppliers)
    {
        UpdateProductDto = updateProductDto;
        Suppliers = suppliers;
    }

    public UpdateProductDTO? UpdateProductDto { get; set; }
    public IEnumerable<Supplier>? Suppliers { get; set; }
}