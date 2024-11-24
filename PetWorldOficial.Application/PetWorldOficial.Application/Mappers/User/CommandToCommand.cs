using AutoMapper;
using PetWorldOficial.Application.Commands.User;

namespace PetWorldOficial.Application.Mappers.User;

public class CommandToCommand : Profile
{
    public CommandToCommand()
    {
        CreateMap<MyAccountCommand, UpdateUserCommand>();
    }
}