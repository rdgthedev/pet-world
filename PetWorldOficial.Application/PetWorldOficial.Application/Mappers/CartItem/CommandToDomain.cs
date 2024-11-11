using AutoMapper;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Commands.CartItem;

namespace PetWorldOficial.Application.Mappers.CartItem;

public class CommandToDomain : Profile
{
    public CommandToDomain()
    {
        CreateMap<DeleteItemCommand, Domain.Entities.CartItem>()
            .ForMember(c => c.Id, options => options.MapFrom(d => d.Id));
    }
}