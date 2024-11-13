using AutoMapper;
using PetWorldOficial.Application.ViewModels;

namespace PetWorldOficial.Application.Mappers.Category;

public class ViewModelToDomain : Profile
{
    public ViewModelToDomain()
    {
        CreateMap<CategoryDetailsViewModel, Domain.Entities.Category>();
    }
}