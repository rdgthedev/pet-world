using AutoMapper;
using MediatR;
using PetWorldOficial.Application.Commands;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Auth;

public class RegisterUserCommandHandler(
    IAuthService authService,
    IUserService userService,
    IMapper mapper) : IRequestHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userExists = await userService.UserExistsAsync(
                request.UserName,
                request.Document,
                request.PhoneNumber,
                request.Email,
                cancellationToken);

            if (userExists.Exists)
                throw new UserAlreadyExistsException(userExists.Error);

            var user = mapper.Map<Domain.Entities.User>(request);
            var userRegistered = await authService.Register(user, request.Role, request.Password);

            if (userRegistered is null)
                throw new UnableToRegisterUserException("Não foi possível registrar o usuário!");

            await authService.SignIn(user);
            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}