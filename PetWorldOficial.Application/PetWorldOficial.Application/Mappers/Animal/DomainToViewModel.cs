using AutoMapper;
using PetWorldOficial.Application.ViewModels.Animal;

namespace PetWorldOficial.Application.Mappers.Animal
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<Domain.Entities.Animal, AnimalDetailsViewModel>()
                .ConstructUsing(a => new AnimalDetailsViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Gender = a.Gender.ToString(),
                    CreatedAt = a.CreatedAt,
                    LastUpdatedAt = a.LastUpdatedAt,
                    ImageUrl = a.ImageUrl!,
                    Race = a.Race,
                    Category = a.Category,
                    Owner = a.Owner,
                });
        }
    }
}
