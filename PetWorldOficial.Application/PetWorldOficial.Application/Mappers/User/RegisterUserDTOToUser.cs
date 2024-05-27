using AutoMapper;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Mappers;

public class RegisterUserDTOToUser : Profile
{
    public RegisterUserDTOToUser()
    {
        CreateMap<RegisterUserDTO, User>()
            .ConstructUsing(u =>
                new User(
                    u.Name,
                    u.UserName,
                    u.Gender.ToString(),
                    u.BirthDate,
                    u.Document,
                    u.Email,
                    u.PhoneNumber,
                    u.Street,
                    u.Number,
                    u.PostalCode,
                    u.Neighborhood,
                    u.Complement,
                    u.City,
                    u.State));
    }
}