using AutoMapper;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Cart;

namespace PetWorldOficial.Application.Mappers.Cart;

public class DomainToCommand : Profile
{
    public DomainToCommand()
    {
        CreateMap<Domain.Entities.Cart, UpdateCartCommand>();
        CreateMap<Domain.Entities.Cart, DeleteCartCommand>()
            .ForMember(dest => dest.CartId, 
                options => options.MapFrom(c => c.Id));
    }
}