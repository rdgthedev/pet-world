using AutoMapper;
using PetWorldOficial.Application.Commands.Product;

namespace PetWorldOficial.Application.Mappers.Product;

public class DomainToCommand : Profile
{
    public DomainToCommand()
    {
        CreateMap<Domain.Entities.Product, UpdateProductCommand>()
            .ConstructUsing(p => new UpdateProductCommand
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                SupplierId = p.SupplierId,
                QuantityInStock = p.Stock.Quantity
            });
    }
}