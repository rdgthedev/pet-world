using AutoMapper;
using PetWorldOficial.Application.Commands.User;

namespace PetWorldOficial.Application.Mappers.User;

public class CommandToDomain : Profile
{
    public CommandToDomain()
    {
        CreateMap<RegisterUserCommand, Domain.Entities.User>();
        CreateMap<UpdateUserCommand, Domain.Entities.User>();
        CreateMap<DeleteUserCommand, Domain.Entities.User>();
        CreateMap<LoginUserCommand, Domain.Entities.User>();
    }
}