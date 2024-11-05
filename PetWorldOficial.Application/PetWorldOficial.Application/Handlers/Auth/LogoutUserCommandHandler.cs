using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;

namespace PetWorldOficial.Application.Handlers.Auth;

public class LogoutUserCommandHandler(
    IAuthService authService,
    ICartCookieService cartCookieService,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<LogoutUserCommand, Unit>
{
    public async Task<Unit> Handle(
        LogoutUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await authService.Logout();
            cartCookieService.SetCartCookie(string.Empty, DateTime.Now.AddDays(-1), httpContextAccessor.HttpContext);
            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}