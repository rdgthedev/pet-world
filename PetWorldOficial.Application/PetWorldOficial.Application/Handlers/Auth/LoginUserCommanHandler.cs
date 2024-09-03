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
            var user = await userService.GetByUserNameAsync(request.UserName, cancellationToken);

            if (user is null)
                throw new LoginInvalidException("Login ou senha inválidos!");

            var result = await authService.Login(mapper.Map<Domain.Entities.User>(user), request.Password);

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