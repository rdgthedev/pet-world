using AutoMapper;
using PetWorldOficial.Application.DTOs.Product.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class ProductToOutputProductDTO : Profile
{
    public ProductToOutputProductDTO()
    {
        CreateMap<Product, OutputProductDTO>().ReverseMap();
    }
}