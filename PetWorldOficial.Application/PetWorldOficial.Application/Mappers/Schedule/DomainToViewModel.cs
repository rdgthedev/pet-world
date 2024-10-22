using AutoMapper;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Schedulling, ScheduleDetailsViewModel>()
            .ConstructUsing(s => new ScheduleDetailsViewModel
            {
                Id = s.Id,
                AnimalId = s.AnimalId,
                ServiceId = s.ServiceId,
                EmployeeId = s.EmployeeId,
                Code = s.Code,
                Date = s.Date,
                Time = s.Time,
                Observation = s.Observation,
                Service = s.Service,
                Animal = s.Animal,
                Employee = s.Employee,
            });
    }
}