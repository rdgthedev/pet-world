using AutoMapper;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Mappers.Cart;

public class ViewModelToCommand : Profile
{
    public ViewModelToCommand()
    {
        CreateMap<CartDetailsViewModel, UpdateCartCommand>();
    }
}