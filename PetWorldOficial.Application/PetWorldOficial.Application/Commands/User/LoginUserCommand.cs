using MediatR;

namespace PetWorldOficial.Application.Commands.User;

public record LoginUserCommand(
    string UserName,
    string Password,
    bool? RememberMe) : IRequest<Unit>
{
}