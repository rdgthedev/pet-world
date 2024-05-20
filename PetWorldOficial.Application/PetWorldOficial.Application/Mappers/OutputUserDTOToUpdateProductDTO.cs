using AutoMapper;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Application.DTOs.Product.Output;

namespace PetWorldOficial.Application.Mappers;

public class OutputUserDTOToUpdateProductDTO : Profile
{
    public OutputUserDTOToUpdateProductDTO()
    {
        CreateMap<OutputProductDTO, UpdateProductDTO>()
            .ConstructUsing(p => 
                new UpdateProductDTO
                {
                    Id = p.Id, 
                    Name = p.Name,
                    Description = p.Description, 
                    Price = p.Price, 
                    SupplierId = p.SupplierId,
                    ImageUrl = p.Image
                }).ReverseMap();
    }
}