using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;

namespace PetWorldOficial.Application.Mappers.Product;

public class DeleteProductDTOToProduct : Profile
{
    public DeleteProductDTOToProduct()
    {
        CreateMap<Domain.Entities.Product, DeleteProductDTO>().ReverseMap();
    }
}