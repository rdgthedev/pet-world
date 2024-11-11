using AutoMapper;
using PetWorldOficial.Application.ViewModels.CartItem;

namespace PetWorldOficial.Application.Mappers.CartItem;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Domain.Entities.CartItem, CartItemDetailsViewModel>();
    }
}