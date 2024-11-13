using AutoMapper;
using PetWorldOficial.Application.ViewModels.Product;

namespace PetWorldOficial.Application.Mappers.Product;

public class ViewModelToDomain : Profile
{
    public ViewModelToDomain()
    {
        CreateMap<ProductDetailsViewModel, Domain.Entities.Product>();
    }
}