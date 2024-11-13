using AutoMapper;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Mappers.Cart;

public class ViewModelToDomain : Profile
{
    public ViewModelToDomain()
    {
        CreateMap<CartDetailsViewModel, Domain.Entities.Cart>();
    }
}