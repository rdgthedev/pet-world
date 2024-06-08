using AutoMapper;
using PetWorldOficial.Application.ViewModels.Schedule;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class ScheduleEntityToViewModelsProfiles : Profile
{
    public ScheduleEntityToViewModelsProfiles()
    {
        CreateMap<Domain.Entities.Schedule, ScheduleDatailsViewModel>()
            .ConstructUsing(s => new ScheduleDatailsViewModel
            {
                Id = s.Id,
                AnimalId = s.AnimalId,
                ServiceId = s.ServiceId,
                Date = s.Date,
                Time = s.Time,
                Observation = s.Observation,
                Service = s.Service,
                Animal = s.Animal
            });

        CreateMap<Domain.Entities.Schedule, DeleteScheduleViewModel>()
            .ConstructUsing(s => new DeleteScheduleViewModel
            {
                AnimalId = s.AnimalId,
                ServiceId = s.ServiceId,
                Date = s.Date,
                Time = s.Time
            });
        
        CreateMap<Domain.Entities.Schedule, UpdateScheduleViewModel>()
            .ConstructUsing(s => new UpdateScheduleViewModel
            {
                // AnimalId = s.AnimalId,
                ServiceId = s.ServiceId,
                Date = s.Date,
                Time = s.Time
            });
    }
}