using AutoMapper;
using PetWorldOficial.Application.Commands.User;

namespace PetWorldOficial.Application.Mappers.User;

public class CommandToDomain : Profile
{
    public CommandToDomain()
    {
        CreateMap<RegisterUserCommand, Domain.Entities.User>();
    }
}