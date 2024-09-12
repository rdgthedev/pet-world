using AutoMapper;
using PetWorldOficial.Application.ViewModels.Race;

namespace PetWorldOficial.Application.Mappers.Race
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<Domain.Entities.Race, RaceDetailsViewModel>();
        }
    }
}
