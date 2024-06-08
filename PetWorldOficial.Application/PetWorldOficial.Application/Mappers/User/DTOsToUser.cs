using AutoMapper;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Application.DTOs.User.Output;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Mappers.User;

public class DTOsToUser : Profile
{
    public DTOsToUser()
    {
        CreateMap<OutputUserDto, Domain.Entities.User>();

        CreateMap<RegisterUserDTO, Domain.Entities.User>()
            .ConstructUsing(dto =>
                new Domain.Entities.User(
                    dto.Name,
                    dto.UserName,
                    dto.Gender,
                    (DateTime)dto.BirthDate!,
                    dto.Document,
                    dto.Email,
                    dto.PhoneNumber,
                    dto.Street,
                    dto.Number,
                    dto.PostalCode,
                    dto.Neighborhood,
                    dto.Complement,
                    dto.City,
                    dto.State));
    }
}