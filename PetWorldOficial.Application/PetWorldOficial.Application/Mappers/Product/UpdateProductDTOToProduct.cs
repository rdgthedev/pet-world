using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;

namespace PetWorldOficial.Application.Mappers.Product;

public class UpdateProductDTOToProduct : Profile
{
    public UpdateProductDTOToProduct()
    {
        CreateMap<UpdateProductDTO, Domain.Entities.Product>()
            .ForMember(prod => prod.Id,
                prodDto=>
                {
                    prodDto.MapFrom(orig => orig.Id);
                })
            .ForMember(prod => prod.Name,
                prodDto=>
                {
                    prodDto.MapFrom(orig => orig.Name);
                })
            .ForMember(prod => prod.Description,
                prodDto=>
                {
                    prodDto.MapFrom(orig => orig.Description);
                })
            .ForMember(prod => prod.ImageUrl,
                prodDto=>
                {
                    prodDto.MapFrom(orig => orig.ImageUrl);
                })
            .ForMember(prod => prod.Price,
                prodDto=>
                {
                    prodDto.MapFrom(orig => orig.Price);
                })
            .ForMember(prod => prod.SupplierId,
                prodDto=>
                {
                    prodDto.MapFrom(orig => orig.SupplierId);
                }).ReverseMap();
    }
}