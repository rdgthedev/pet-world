using MediatR;

namespace PetWorldOficial.Application.Commands.User;

public class ResetPasswordCommand : IRequest<(int statusCode, string message)>
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty; 
}