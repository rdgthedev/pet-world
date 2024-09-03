using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.DTOs.User.Output;

public class OutputUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string Complement { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}