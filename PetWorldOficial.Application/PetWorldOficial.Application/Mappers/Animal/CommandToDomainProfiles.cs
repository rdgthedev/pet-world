using AutoMapper;
using PetWorldOficial.Application.Commands.Animal;

namespace PetWorldOficial.Application.Mappers.Animal;

public class CommandToDomainProfiles : Profile
{
    public CommandToDomainProfiles()
    {
        CreateMap<RegisterAnimalCommand, Domain.Entities.Animal>().ReverseMap();
        CreateMap<DeleteAnimalCommand, Domain.Entities.Animal>().ReverseMap();
        CreateMap<UpdateAnimalCommand, Domain.Entities.Animal>().ReverseMap();
    }
}