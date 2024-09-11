using AutoMapper;
using PetWorldOficial.Application.Commands.Schedule;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class CommandToDomainProfiles : Profile
{
    public CommandToDomainProfiles()
    {
        CreateMap<CreateScheduleCommand, Domain.Entities.Schedulling>();
    }
}