using AutoMapper;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Mappers.Animal;

public class CommandToDomainProfiles : Profile
{
    public CommandToDomainProfiles()
    {
        CreateMap<RegisterAnimalCommand, Domain.Entities.Animal>()
            .ConstructUsing(cmd => new Domain.Entities.Animal(
                cmd.Name,
                (EGender)Enum.Parse(typeof(EGender), cmd.Gender),
                cmd.RaceId,
                cmd.CategoryId,
                cmd.UserId));

        CreateMap<DeleteAnimalCommand, Domain.Entities.Animal>()
            .ConstructUsing(cmd => new Domain.Entities.Animal(
                cmd.Name,
                (EGender)Enum.Parse(typeof(EGender), cmd.Gender),
                cmd.RaceId,
                cmd.CategoryId,
                cmd.UserId))
            .ReverseMap();

        //CreateMap<UpdateAnimalCommand, Domain.Entities.Animal>().ReverseMap();
    }
}