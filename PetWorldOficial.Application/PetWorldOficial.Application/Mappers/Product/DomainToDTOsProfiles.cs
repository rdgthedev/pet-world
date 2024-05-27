using AutoMapper;
using PetWorldOficial.Application.DTOs.Product.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class DomainToDTOsProfiles : Profile
{
    public DomainToDTOsProfiles()
    {
        CreateMap<Product, OutputProductDTO>();
    }
}