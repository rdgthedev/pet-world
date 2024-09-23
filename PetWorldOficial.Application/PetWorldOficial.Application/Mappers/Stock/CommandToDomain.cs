using AutoMapper;
using PetWorldOficial.Application.Commands.Product;

namespace PetWorldOficial.Application.Mappers.Stock
{
    public class CommandToDomain : Profile
    {
        public CommandToDomain()
        {
            CreateMap<CreateProductCommand, Domain.Entities.Stock>()
                .ConstructUsing(cpc => new Domain.Entities.Stock(
                    cpc.ProductId,
                    cpc.Quantity!.Value));
            
            CreateMap<UpdateProductCommand, Domain.Entities.Stock>()
                .ConstructUsing(cpc => new Domain.Entities.Stock(
                    cpc.ProductId,
                    cpc.QuantityInStock!.Value));
        }
    }
}
