using AutoMapper;
using PetWorldOficial.Application.DTOs.Schedule;
using PetWorldOficial.Application.DTOs.Schedule.Output;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class DTOsToScheduleEntity : Profile
{
    public DTOsToScheduleEntity()
    {
        CreateMap<OutputScheduleDTO, Domain.Entities.Schedule>()
            .ConstructUsing(dto => new Domain.Entities.Schedule(
                dto.AnimalId,
                dto.ServiceId,
                dto.Date,
                dto.Time,
                dto.Observation));

        CreateMap<DeleteScheduleDTO, Domain.Entities.Schedule>()
            .ConstructUsing(dto => new Domain.Entities.Schedule(
                dto.AnimalId,
                dto.ServiceId,
                dto.Date,
                dto.Time,
                null));
    }
}