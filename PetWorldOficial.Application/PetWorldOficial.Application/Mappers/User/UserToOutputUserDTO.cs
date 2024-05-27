using AutoMapper;
using PetWorldOficial.Application.DTOs.User.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class UserToOutputUserDTO : Profile
{
    public UserToOutputUserDTO()
    {
        CreateMap<User, OutputUserDto>().ReverseMap();
    }
}