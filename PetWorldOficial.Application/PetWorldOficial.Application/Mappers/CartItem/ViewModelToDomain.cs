using AutoMapper;
using PetWorldOficial.Application.ViewModels.CartItem;

namespace PetWorldOficial.Application.Mappers.CartItem;

public class ViewModelToDomain : Profile
{
    public ViewModelToDomain()
    {
        CreateMap<CartItemDetailsViewModel, Domain.Entities.CartItem>();
    }
}