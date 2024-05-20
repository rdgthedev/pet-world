using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class DeleteProductDTOToProduct : Profile
{
    public DeleteProductDTOToProduct()
    {
        CreateMap<Product, DeleteProductDTO>().ReverseMap();
    }
}