using AutoMapper;
using PetWorldOficial.Application.DTOs.Product.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class DTOsToDomainProfiles : Profile
{
    public DTOsToDomainProfiles()
    {
        CreateMap<OutputProductDTO, Product>();
    }
}