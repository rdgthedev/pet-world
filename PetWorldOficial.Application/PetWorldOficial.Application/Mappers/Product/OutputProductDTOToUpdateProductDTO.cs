using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Application.DTOs.Product.Output;

namespace PetWorldOficial.Application.Mappers.Product;

public class OutputProductDTOToUpdateProductDTO : Profile
{
    public OutputProductDTOToUpdateProductDTO()
    {
        CreateMap<OutputProductDTO, UpdateProductDTO>()
            .ConstructUsing(p => 
                new UpdateProductDTO(
                    p.Id, 
                    p.Name,
                    p.Description,
                    p.ImageUrl,
                    p.Price, 
                    p.SupplierId)).ReverseMap();
    }
}