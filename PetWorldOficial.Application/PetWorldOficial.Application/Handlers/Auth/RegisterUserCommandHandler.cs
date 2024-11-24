using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Auth;

public class RegisterUserCommandHandler(
    IAuthService authService,
    IUserService userService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userExists = await userService.UserExistsAsync(
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

            if (!(bool)httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated)
                await authService.SignIn(user);

            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}