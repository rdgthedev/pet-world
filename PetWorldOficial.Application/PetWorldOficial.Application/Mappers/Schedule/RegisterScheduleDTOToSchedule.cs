using AutoMapper;
using PetWorldOficial.Application.DTOs.Schedule.Input;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class RegisterScheduleDTOToSchedule : Profile
{
    // public RegisterScheduleDTOToSchedule()
    // {
    //     CreateMap<RegisterScheduleDTO, Domain.Entities.Schedule>()
    //         .ConstructUsing(s => new Domain.Entities.Schedule(
    //                 s.AnimalId, 
    //                 s.ServiceId,
    //                 s.Date,
    //                 s.Time, 
    //                 s.Observation));
    // }
}