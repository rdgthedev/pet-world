using AutoMapper;
using PetWorldOficial.Application.DTOs.Service.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class ServiceToOutputServiceDTO : Profile
{
    public ServiceToOutputServiceDTO()
    {
        CreateMap<OutputServiceDTO, Service>().ReverseMap();
    }
}