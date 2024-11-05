using AutoMapper;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Mappers.Cart;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Domain.Entities.Cart, CartDetailsViewModel>();
    }
}