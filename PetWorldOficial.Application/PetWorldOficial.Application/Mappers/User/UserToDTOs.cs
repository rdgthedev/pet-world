using AutoMapper;
using PetWorldOficial.Application.DTOs.User.Output;

namespace PetWorldOficial.Application.Mappers.User;

public class UserToDTOs : Profile
{
    public UserToDTOs()
    {
        CreateMap<Domain.Entities.User, OutputUserDto>();
    }
}