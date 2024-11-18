using AutoMapper;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class ViewModelToDomain : Profile
{
    public ViewModelToDomain()
    {
        CreateMap<ScheduleDetailsViewModel, Schedulling>();
    }
}