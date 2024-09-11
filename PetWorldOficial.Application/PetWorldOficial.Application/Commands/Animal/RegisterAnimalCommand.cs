using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Commands.Animal;

public record RegisterAnimalCommand : IRequest<RegisterAnimalCommand>
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A raça é obrigatória!")]
    public string Race { get; set; } = string.Empty;

    [Required(ErrorMessage = "O gênero é obrigatório!")]
    public string Gender { get; set; } = string.Empty;

    [Required] 
    public int UserId { get; set; }
    public IEnumerable<UserDetailsViewModel?> Users { get; set; } = Enumerable.Empty<UserDetailsViewModel>();
    public string? Message { get; set; }
    public ClaimsPrincipal? UserPrincipal { get; set; }
}