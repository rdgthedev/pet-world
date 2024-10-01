using AutoMapper;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Schedulling, ScheduleDetailsViewModel>();
    }
}