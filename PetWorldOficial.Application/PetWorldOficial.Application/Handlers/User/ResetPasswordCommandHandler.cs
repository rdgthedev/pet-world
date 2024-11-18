using AutoMapper;
using MediatR;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.User;

public class ResetPasswordCommandHandler(
    IUserService userService,
    IAuthService authService,
    IMapper mapper) : IRequestHandler<ResetPasswordCommand, (int statusCode, string message)>
{
    public async Task<(int statusCode, string message)> Handle(ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Usuário não encontrado!");

            await authService.ResetPassword(mapper.Map<Domain.Entities.User>(user), request.Token, request.Password);
            return (statusCode: 200, message: "Senha alterada com sucesso!");
        }
        catch (Exception)
        {
            throw;
        }
    }
}