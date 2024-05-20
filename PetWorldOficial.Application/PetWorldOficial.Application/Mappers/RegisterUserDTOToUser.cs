using AutoMapper;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class RegisterUserDTOToUser : Profile
{
    public RegisterUserDTOToUser()
    {
        CreateMap<User, RegisterUserDTOToUser>();
    }
}