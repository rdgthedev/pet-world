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
                (EPetGender)Enum.Parse(typeof(EPetGender), cmd.Gender),
                cmd.RaceName,
                cmd.CategoryId!.Value,
                cmd.UserId!.Value,
                cmd.ImageUrl));

        CreateMap<DeleteAnimalCommand, Domain.Entities.Animal>()
            .ConstructUsing(cmd => new Domain.Entities.Animal(
                cmd.Name,
                (EPetGender)Enum.Parse(typeof(EPetGender), cmd.Gender),
                cmd.RaceName,
                cmd.CategoryId,
                cmd.UserId,
                cmd.ImageUrl));

        CreateMap<UpdateAnimalCommand, Domain.Entities.Animal>()
            .ConstructUsing(cmd => new Domain.Entities.Animal(
                cmd.Name,
                (EPetGender)Enum.Parse(typeof(EPetGender), cmd.Gender),
                cmd.RaceName,
                cmd.CategoryId!.Value,
                cmd.UserId!.Value,
                cmd.ImageUrl));

        //CreateMap<UpdateAnimalCommand, Domain.Entities.Animal>().ReverseMap();
    }
}