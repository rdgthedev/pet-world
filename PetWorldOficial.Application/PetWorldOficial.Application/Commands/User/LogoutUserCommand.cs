using MediatR;

namespace PetWorldOficial.Application.Commands.User;

public record LogoutUserCommand() : IRequest<Unit>
{
}