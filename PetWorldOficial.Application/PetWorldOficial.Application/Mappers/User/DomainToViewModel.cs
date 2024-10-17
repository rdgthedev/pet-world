using AutoMapper;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Mappers.User;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Domain.Entities.User, UserDetailsViewModel>();
    }
}