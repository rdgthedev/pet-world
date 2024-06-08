using AutoMapper;
using PetWorldOficial.Application.ViewModels.Animal;

namespace PetWorldOficial.Application.Mappers.Animal;

public class AnimalEntityToViewModelsProfiles : Profile
{
    public AnimalEntityToViewModelsProfiles()
    {
        CreateMap<Domain.Entities.Animal, AnimalDetailsViewModel>()
            .ConstructUsing(c =>
                new AnimalDetailsViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Gender = c.Gender.ToString(),
                    Species = c.Species,
                    Race = c.Race,
                    UserId = c.UserId,
                    User = c.User
                });
    }
}