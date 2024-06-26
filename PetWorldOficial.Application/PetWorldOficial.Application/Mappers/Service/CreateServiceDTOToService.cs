﻿using AutoMapper;
using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class CreateServiceDTOToService : Profile
{
    public CreateServiceDTOToService()
    {
        CreateMap<Domain.Entities.Service, CreateServiceDTO>().ReverseMap();
    }
}