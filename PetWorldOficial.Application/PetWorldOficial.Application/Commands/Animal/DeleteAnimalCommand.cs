using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;

namespace PetWorldOficial.Application.Commands.Animal;

public record DeleteAnimalCommand([Required] int Id) : IRequest<DeleteAnimalCommand>
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A raça é obrigatória!")]
    public int RaceId { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório!")]
    public string Gender { get; set; } = string.Empty;
    public string CategoryName { get; set; }
    public string RaceName { get; set; }

    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public int? Age { get; set; }

    public ClaimsPrincipal? UserPrincipal { get; set; }

    public string Message { get; set; } = string.Empty;
}