using AutoMapper;
using MediatR;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Auth;

public class LoginUserCommanHandler(
    IAuthService authService,
    IUserService userService,
    IMapper mapper) : IRequestHandler<LoginUserCommand, Unit>
{
    public async Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await authService.Login(request);

            if (!result)
                throw new LoginInvalidException("Login ou senha inválidos!");

            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}