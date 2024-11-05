using System.ComponentModel.DataAnnotations;
using MediatR;

namespace PetWorldOficial.Application.Commands.User;

public class LoginUserCommand : IRequest<Unit>
{
    [Required(ErrorMessage = "O email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Formato inválido! Utilize algo como: exemplo@dominio.com")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória!")]
    public string Password { get; set; } = string.Empty;
    public bool? RememberMe { get; set; }
    
}