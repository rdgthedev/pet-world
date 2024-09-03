using AutoMapper;
using PetWorldOficial.Application.DTOs.Product.Output;

namespace PetWorldOficial.Application.Mappers.Product;

public class DTOsToDomainProfiles : Profile
{
    public DTOsToDomainProfiles()
    {
        CreateMap<OutputProductDTO, Domain.Entities.Product>();
    }
}