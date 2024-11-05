using AutoMapper;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Mappers.User;

public class ViewModelToDomain : Profile
{
    public ViewModelToDomain()
    {
        CreateMap<UserDetailsViewModel, Domain.Entities.User>();
    }
}