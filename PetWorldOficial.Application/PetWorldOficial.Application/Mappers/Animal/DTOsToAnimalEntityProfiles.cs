using AutoMapper;
using PetWorldOficial.Application.DTOs.Animal.Input;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Mappers;

public class DTOsToAnimalEntityProfiles : Profile
{
    public DTOsToAnimalEntityProfiles()
    {
        CreateMap<RegisterAnimalDTO, Animal>()
            .ForMember(a => a.Gender,
                options
                    => options.MapFrom(dto => (EGender)Enum.Parse(typeof(EGender), dto.Gender)));
    }
}