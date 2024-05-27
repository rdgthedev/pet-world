using AutoMapper;
using PetWorldOficial.Application.DTOs.Schedule.Input;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class RegisterScheduleDTOToSchedule : Profile
{
    public RegisterScheduleDTOToSchedule()
    {
        CreateMap<RegisterScheduleDTO, Schedule>()
            .ConstructUsing(s => new Schedule(s.AnimalId, s.ServiceId, s.Date,s.Time, s.Observation));
    }
}