using AutoMapper;
using PetWorldOficial.Application.DTOs.Schedule;
using PetWorldOficial.Application.DTOs.Schedule.Output;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class DTOsToDTOs : Profile
{
    public DTOsToDTOs()
    {
        CreateMap<OutputScheduleDTO, DeleteScheduleDTO>()
            .ConstructUsing(o => new DeleteScheduleDTO
            {
                Id = o.Id,
                Date = o.Date,
                Time = o.Time,
                AnimalId = o.Animal.Id,
                ServiceId = o.Service.Id
            });
    }
}