using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.ViewModels.Product;

public class DeleteProductViewModel
{
    public PetWorldOficial.Domain.Entities.Product Product { get; set; }
    public Supplier Supplier { get; set; }

    public DeleteProductViewModel(PetWorldOficial.Domain.Entities.Product product, Supplier supplier)
    {
        Product = product;
        Supplier = supplier;
    }
}