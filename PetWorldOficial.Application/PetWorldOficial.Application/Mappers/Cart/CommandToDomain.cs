using AutoMapper;
using PetWorldOficial.Application.Commands.Cart;

namespace PetWorldOficial.Application.Mappers.Cart;

public class CommandToDomain : Profile
{
    public CommandToDomain()
    {
        CreateMap<UpdateCartCommand, Domain.Entities.Cart>();
    }
}