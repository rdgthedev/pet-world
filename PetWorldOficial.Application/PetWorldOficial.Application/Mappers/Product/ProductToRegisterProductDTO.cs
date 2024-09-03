using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;

namespace PetWorldOficial.Application.Mappers.Product;

public class ProductToRegisterProductDTO : Profile
{
    public ProductToRegisterProductDTO()
    {
        CreateMap<RegisterProductDTO, Domain.Entities.Product>()
            .ForMember(prodDTO => prodDTO.Name,
                prod =>
                {
                    prod.MapFrom(orig => orig.Name);
                })
            .ForMember(prodDTO => prodDTO.Description,
                prod =>
                {
                    prod.MapFrom(orig => orig.Description);
                })
            .ForMember(prodDTO => prodDTO.ImageUrl,
                prod =>
                {
                    prod.MapFrom(orig => orig.ImageUrl);
                })
            .ForMember(prodDTO => prodDTO.Price,
                prod =>
                {
                    prod.MapFrom(orig => orig.Price);
                })
            .ForMember(prodDTO => prodDTO.SupplierId,
                prod =>
                {
                    prod.MapFrom(orig => orig.SupplierId);
                }).ReverseMap();
    }
}