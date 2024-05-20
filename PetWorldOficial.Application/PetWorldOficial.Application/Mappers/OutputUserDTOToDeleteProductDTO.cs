using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Application.DTOs.Product.Output;

namespace PetWorldOficial.Application.Mappers;

public class OutputUserDTOToDeleteProductDTO : Profile
{
    public OutputUserDTOToDeleteProductDTO()
    {
        CreateMap<DeleteProductDTO, OutputProductDTO>().ReverseMap();
    }
}