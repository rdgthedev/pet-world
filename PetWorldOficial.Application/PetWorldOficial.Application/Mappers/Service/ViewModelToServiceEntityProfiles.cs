using AutoMapper;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Mappers.Service;

public class ViewModelToServiceEntityProfiles : Profile
{
    public ViewModelToServiceEntityProfiles()
    {
        CreateMap<UpdateServiceViewModel, Domain.Entities.Service>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        
        CreateMap<DeleteServiceViewModel, Domain.Entities.Service>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
    }
}