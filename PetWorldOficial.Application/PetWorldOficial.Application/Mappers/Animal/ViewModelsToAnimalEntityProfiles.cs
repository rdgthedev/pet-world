using AutoMapper;
using PetWorldOficial.Application.DTOs.Animal.Input;
using PetWorldOficial.Application.DTOs.Animal.Output;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Mappers.Animal;

public class ViewModelsToAnimalEntityProfiles : Profile
{
    public ViewModelsToAnimalEntityProfiles()
    {
        CreateMap<CreateAnimalViewModel, Domain.Entities.Animal>()
            .ForMember(a => a.Gender,
                options
                    => options.MapFrom(dto => (EGender)Enum.Parse(typeof(EGender), dto.Gender)));

        CreateMap<AnimalDetailsViewModel, Domain.Entities.Animal>()
            .ConstructUsing(c =>
                new Domain.Entities.Animal(
                    c.Name,
                    c.Species,
                    c.Race,
                    (EGender)Enum.Parse(typeof(EGender), c.Gender),
                    c.UserId));

        CreateMap<DeleteAnimalViewModel, Domain.Entities.Animal>()
            .ConstructUsing(c =>
                new Domain.Entities.Animal(
                    c.Name,
                    c.Species,
                    c.Race,
                    (EGender)Enum.Parse(typeof(EGender), c.Gender),
                    c.UserId));
        
        CreateMap<UpdateAnimalViewModel, Domain.Entities.Animal>()
            .ConstructUsing(c =>
                new Domain.Entities.Animal(
                    c.Name,
                    c.Species,
                    c.Race,
                    (EGender)Enum.Parse(typeof(EGender), c.Gender),
                    c.UserId));
    }
}