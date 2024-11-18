using System.Security.Policy;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.User;

public class ForgotPasswordCommandHandler(
    LinkGenerator linkGenerator,
    IUserService userService,
    IAuthService authService,
    IHttpContextAccessor httpContextAccessor,
    IEmailSenderService emailSenderService,
    IMapper mapper) : IRequestHandler<ForgotPasswordCommand, (int statusCode, string message)>
{
    public async Task<(int statusCode, string message)> Handle(ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Se o email for válido, irá chegar um e-mail em sua caixa de entrada!");

            var token = await authService.GenerateForgetPasswordTokenAsync(mapper.Map<Domain.Entities.User>(user));

            if (string.IsNullOrEmpty(token))
                throw new Exception();

            var callbackUrl = linkGenerator.GetUriByAction(
                httpContextAccessor.HttpContext,
                "ResetPassword",
                "Auth",
                values: new { token = token, email = user.Email },
                scheme: httpContextAccessor.HttpContext.Request.Scheme);

            await emailSenderService.SendAsync(
                user.Email,
                "Redefinição de Senha",
                $"Você solicitou a redefinição de senha. Clique no link abaixo para prosseguir:\n\n" +
                $"{callbackUrl}\n\nSe você não solicitou esta ação, ignore este e-mail.\n\n" +
                $"Atencionsamente,\nEquipe Pet World");

            return (statusCode: 200, message: "Se o email for válido, irá chegar um e-mail em sua caixa de entrada!");
        }
        catch (Exception)
        {
            throw;
        }
    }
}