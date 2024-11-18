using System.ComponentModel.DataAnnotations;
using MediatR;

namespace PetWorldOficial.Application.Commands.User;

public class ForgotPasswordCommand : IRequest<(int statusCode, string message)>
{
    [Required(ErrorMessage = "O email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Formato inválido! Utilize algo como: exemplo@dominio.com")]
    public string Email { get; set; } = string.Empty;
}