using AutoMapper;
using PetWorldOficial.Application.DTOs.Service.Output;

namespace PetWorldOficial.Application.Mappers.Service;

public class ServiceToOutputServiceDTO : Profile
{
    public ServiceToOutputServiceDTO()
    {
        CreateMap<OutputServiceDTO, Domain.Entities.Service>().ReverseMap();
    }
}