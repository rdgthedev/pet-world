using AutoMapper;
using PetWorldOficial.Application.ViewModels.Stock;

namespace PetWorldOficial.Application.Mappers.Stock;

public class ViewModelToDomain : Profile
{
    public ViewModelToDomain()
    {
        CreateMap<StockDetailsViewModel, Domain.Entities.Stock>();
    }
}