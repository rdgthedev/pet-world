using AutoMapper;
using PetWorldOficial.Application.Commands.CartItem;
using PetWorldOficial.Application.ViewModels.CartItem;

namespace PetWorldOficial.Application.Mappers.CartItem;

public class ViewModelToCommand : Profile
{
    public ViewModelToCommand()
    {
        CreateMap<CartItemDetailsViewModel, DeleteItemCommand>();
    }
}