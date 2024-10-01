using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Race;

namespace PetWorldOficial.Application.Commands.Animal;

public record UpdateAnimalCommand([Required] int Id) : IRequest<UpdateAnimalCommand>
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório!")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória")]
    public int CategoryId { get; set; }

    public string CategoryTitle { get; set; }

    [Required(ErrorMessage = "A raça é obrigatória")]
    public int RaceId { get; set; }

    public string RaceName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public IFormFile? File { get; set; }
    public string? ImageUrl { get; set; }
    public int UserId { get; set; }
    public ClaimsPrincipal? UserPrincipal { get; set; }
    public string Message { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

    public IEnumerable<RaceDetailsViewModel> Races { get; set; } = Enumerable.Empty<RaceDetailsViewModel>();

    public IEnumerable<CategoryDetailsViewModel> Categories { get; set; } =
        Enumerable.Empty<CategoryDetailsViewModel>();
}