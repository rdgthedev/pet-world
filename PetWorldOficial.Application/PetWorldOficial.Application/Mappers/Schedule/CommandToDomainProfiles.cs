using AutoMapper;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class CommandToDomainProfiles : Profile
{
    public CommandToDomainProfiles()
    {
        CreateMap<CreateScheduleCommand, Schedulling>()
            .ConstructUsing(s => new Schedulling(
                s.AnimalId!.Value,
                s.EmployeeId!.Value,
                s.ServiceId,
                s.Date!.Value,
                s.Time!.Value,
                s.Observation,
                s.Code));

        CreateMap<UpdateSchedulingCommand, Schedulling>()
            .ConstructUsing(s => new Schedulling(
                s.AnimalId!.Value,
                s.EmployeeId!.Value,
                s.ServiceId,
                s.Date!.Value,
                s.Time!.Value,
                s.Observation,
                s.Code));
    }
}