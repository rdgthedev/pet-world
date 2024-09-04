using MediatR;

namespace PetWorldOficial.Application.Commands.User;

public record DeleteUserCommand(int Id) : IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}