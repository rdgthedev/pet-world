using MediatR;

namespace PetWorldOficial.Application.Commands.User;

public record RegisterUserCommand(
    string Name,
    string UserName,
    string Gender,
    DateTime? BirthDate,
    string Document,
    string? Role,
    string Password,
    string Email,
    string PhoneNumber,
    string Street,
    int Number,
    string PostalCode,
    string Neighborhood,
    string Complement,
    string City,
    string State) : IRequest<Unit>
{
}