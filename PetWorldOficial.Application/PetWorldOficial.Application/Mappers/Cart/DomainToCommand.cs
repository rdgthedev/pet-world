using AutoMapper;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Cart;

namespace PetWorldOficial.Application.Mappers.Cart;

public class DomainToCommand : Profile
{
    public DomainToCommand()
    {
        CreateMap<Domain.Entities.Cart, UpdateCartCommand>()
            .ConstructUsing(c => new UpdateCartCommand
            {
                Id = c.Id,
                ClientId = c.ClientId,
                Items = c.Items
            });
        CreateMap<Domain.Entities.Cart, DeleteCartCommand>()
            .ForMember(dest => dest.Id,
                options => options.MapFrom(c => c.Id));
    }
}