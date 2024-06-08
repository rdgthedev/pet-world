using AutoMapper.Internal.Mappers;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.DTOs.User.Input;

public record RegisterUserDTO(
    string Name,
    string UserName,
    string Gender,
    DateTime? BirthDate,
    string Document,
    string Password,
    string Email,
    string PhoneNumber,
    string Street,
    int Number,
    string PostalCode,
    string Neighborhood,
    string Complement,
    string City,
    string State);