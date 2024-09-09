using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.Service;

public record ServiceDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O preço é obrigatório!")]
    public double Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
}