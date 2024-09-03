using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Application.DTOs.Product.Output;

namespace PetWorldOficial.Application.Mappers.Product;

public class OutputProductDTOToDeleteProductDTO : Profile
{
    public OutputProductDTOToDeleteProductDTO()
    {
        CreateMap<DeleteProductDTO, OutputProductDTO>().ReverseMap();
    }
}