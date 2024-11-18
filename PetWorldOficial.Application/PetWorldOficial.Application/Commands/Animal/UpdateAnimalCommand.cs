using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Race;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Commands.Animal;

public record UpdateAnimalCommand([Required] int Id) : IRequest<UpdateAnimalCommand>
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A raça é obrigatória!")]
    public string RaceName { get; set; } = string.Empty;
    // public int? RaceId { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório!")]
    public string Gender { get; set; } = string.Empty;

    public IFormFile? File { get; set; }
    public string? ImageUrl { get; set; }
    public string BaseUrl { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    [Required(ErrorMessage = "O dono é obrigatório!")]
    public int? UserId { get; set; }
    public int? AdminId { get; set; }
    [Required(ErrorMessage = "A categoria é obrigatória!")]
    public int? CategoryId { get; set; }

    public string CategoryTitle { get; set; } = String.Empty;


    public IEnumerable<UserDetailsViewModel?> Users { get; set; } = Enumerable.Empty<UserDetailsViewModel>();
    public IEnumerable<RaceDetailsViewModel?> Races { get; set; } = Enumerable.Empty<RaceDetailsViewModel>();
    public IEnumerable<CategoryDetailsViewModel?> Categories { get; set; } = Enumerable.Empty<CategoryDetailsViewModel>();

    public string? Message { get; set; }
    public ClaimsPrincipal? UserPrincipal { get; set; }
}