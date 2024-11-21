using AutoMapper;
using PetWorldOficial.Application.ViewModels.Stock;

namespace PetWorldOficial.Application.Mappers.Stock;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Domain.Entities.Stock, StockDetailsViewModel>();
    }
}