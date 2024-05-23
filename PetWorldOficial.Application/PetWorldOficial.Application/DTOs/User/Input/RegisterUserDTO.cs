using AutoMapper.Internal.Mappers;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.DTOs.User.Input;

public class RegisterUserDTO
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public EGender Gender { get; set; } 
    public DateTime BirthDate { get; set; }
    public string Document { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string PostalCode { get; set; }
    public string Neighborhood { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}