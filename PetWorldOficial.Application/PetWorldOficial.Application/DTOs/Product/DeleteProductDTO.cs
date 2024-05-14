using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.DTOs.Product;

public class DeleteProductDTO
{
    public Domain.Entities.Product Product { get; set; }
    public Supplier Supplier { get; set; }

    public DeleteProductDTO(Domain.Entities.Product product, Supplier supplier)
    {
        Product = product;
        Supplier = supplier;
    }
}