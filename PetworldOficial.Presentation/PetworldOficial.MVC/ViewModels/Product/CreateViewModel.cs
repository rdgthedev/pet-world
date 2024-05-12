using PetWorldOficial.Application.DTOs.Product.Input;
using PetWorldOficial.Domain.Entities;

namespace PetworldOficial.MVC.ViewModels.Product;

public class CreateViewModel
{
    public ProductRegisterDTO Product { get; set; }
    public IEnumerable<Supplier>? Suppliers { get; set; }

    public CreateViewModel(
        ProductRegisterDTO? product,
        IEnumerable<Supplier>? suppliers)
    {
        Product = product!;
        Suppliers = suppliers;
    }
}