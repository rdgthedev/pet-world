using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class ProductToProductRegisterDTO : Profile
{
    public ProductToProductRegisterDTO()
    {
        CreateMap<RegisterProductDTO, Product>()
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
            .ForMember(prodDTO => prodDTO.Image,
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