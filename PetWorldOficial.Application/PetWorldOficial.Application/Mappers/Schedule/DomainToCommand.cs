using AutoMapper;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class DomainToCommand : Profile
{
    public DomainToCommand()
    {
        CreateMap<Schedulling, DeleteSchedulingCommand>()
            .ConstructUsing(s => new DeleteSchedulingCommand(s.Id)
            {
                AnimalName = s.Animal.Name,
                DurationInMinutes = s.Service.DurationInMinutes,
                EmployeeName = s.Employee.Name,
                Observation = s.Observation,
                SchedulingDate = s.Date,
                SchedulingTime = s.Time,
                ServiceName = s.Service.Name,
                ServicePrice = s.Service.Price
            });
    }
}