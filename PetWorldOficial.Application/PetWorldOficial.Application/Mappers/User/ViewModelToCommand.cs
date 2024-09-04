using AutoMapper;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Mappers.User;

public class ViewModelToCommand : Profile
{
    public ViewModelToCommand()
    {
        CreateMap<UserDetailsViewModel, UpdateUserCommand>();
        CreateMap<UserDetailsViewModel, DeleteUserCommand>();
    }
}