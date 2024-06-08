using AutoMapper;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Mappers.User;

public class RegisterUserDTOToUser : Profile
{
    public RegisterUserDTOToUser()
    {
        CreateMap<RegisterUserDTO, Domain.Entities.User>().ForMember(a => a.Gender,
            options
                => options.MapFrom(dto => (EGender)Enum.Parse(typeof(EGender), dto.Gender)));
    }
}