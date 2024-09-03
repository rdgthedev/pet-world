using AutoMapper;
using PetWorldOficial.Application.DTOs.Product.Output;

namespace PetWorldOficial.Application.Mappers.Product;

public class DomainToDTOsProfiles : Profile
{
    public DomainToDTOsProfiles()
    {
        CreateMap<Domain.Entities.Product, OutputProductDTO>();
    }
}