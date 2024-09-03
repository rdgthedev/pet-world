using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;

namespace PetWorldOficial.Application.Commands.Animal;

public record DeleteAnimalCommand([Required] int Id) : IRequest<DeleteAnimalCommand>
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "A espécie é obrigatória!")]
    public string Species { get; set; }

    [Required(ErrorMessage = "A raça é obrigatória!")]
    public string Race { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório!")]
    public string Gender { get; set; }

    [Required] public int UserId { get; set; }

    public ClaimsPrincipal? UserPrincipal { get; set; }

    public string Message { get; set; } = string.Empty;
}