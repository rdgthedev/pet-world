using AutoMapper;
using PetWorldOficial.Application.Commands.Product;

namespace PetWorldOficial.Application.Mappers.Product;

public class CommandToDomain : Profile
{
    public CommandToDomain()
    {
        CreateMap<CreateProductCommand, Domain.Entities.Product>()
            .ConstructUsing(cpc => new Domain.Entities.Product(
            cpc.Name,
            cpc.Description,
            cpc.Price!.Value,
            cpc.ImageUrl,
            cpc.SupplierId,
            cpc.CategoryId));

        CreateMap<DeleteProductCommand, Domain.Entities.Product>();
            //.ConstructUsing(cpc => new Domain.Entities.Product(
            //    cpc.Name,
            //    cpc.Description,
            //    cpc.Price,
            //    cpc.ImageUrl,
            //    cpc.SupplierId,
            //    cpc.CategoryId));
    }
}