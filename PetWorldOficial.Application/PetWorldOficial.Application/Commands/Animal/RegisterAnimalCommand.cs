using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Race;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Commands.Animal;

public record RegisterAnimalCommand : IRequest<RegisterAnimalCommand>
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A raça é obrigatória!")]
    public int RaceId { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório!")]
    public string Gender { get; set; } = string.Empty;

    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public int? Age { get; set; }

    public IEnumerable<UserDetailsViewModel?> Users { get; set; } = Enumerable.Empty<UserDetailsViewModel>();
    public IEnumerable<RaceDetailsViewModel?> Races { get; set; } = Enumerable.Empty<RaceDetailsViewModel>();
    public IEnumerable<CategoryDetailsViewModel?> Categories { get; set; } = Enumerable.Empty<CategoryDetailsViewModel>();

    public string? Message { get; set; }
    public ClaimsPrincipal? UserPrincipal { get; set; }
}