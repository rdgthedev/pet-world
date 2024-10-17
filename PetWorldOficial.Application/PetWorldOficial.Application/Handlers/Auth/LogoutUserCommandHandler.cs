using MediatR;
using PetWorldOficial.Application.Commands;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;

namespace PetWorldOficial.Application.Handlers.Auth;

public class LogoutUserCommandHandler(IAuthService authService)
    : IRequestHandler<LogoutUserCommand, Unit>
{
    public async Task<Unit> Handle(
        LogoutUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await authService.Logout();
            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}