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

        CreateMap<UpdateProductCommand, Domain.Entities.Product>();
        // .ConstructUsing(s => new Domain.Entities.Product(
        //     s.Name,
        //     s.Description,
        //     s.Price!.Value,
        //     s.ImageUrl,
        //     s.SupplierId,
        //     s.CategoryId));
    }
}