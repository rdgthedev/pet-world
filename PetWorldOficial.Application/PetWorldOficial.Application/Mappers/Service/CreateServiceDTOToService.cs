using AutoMapper;
using PetWorldOficial.Application.DTOs.Service;

namespace PetWorldOficial.Application.Mappers.Service;

public class CreateServiceDTOToService : Profile
{
    public CreateServiceDTOToService()
    {
        CreateMap<Domain.Entities.Service, CreateServiceDTO>().ReverseMap();
    }
}