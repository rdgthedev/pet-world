using AutoMapper;
using PetWorldOficial.Application.Commands.Product;

namespace PetWorldOficial.Application.Mappers.Product;

public class CommandToDomain : Profile
{
    public CommandToDomain()
    {
        CreateMap<CreateProductCommand, Domain.Entities.Product>()
            .ForMember(d => d.LastUpdatedAt, opt => opt.Ignore());

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