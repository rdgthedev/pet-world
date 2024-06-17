using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.Service;

public class UpdateServiceViewModel
{
    [Required] public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }

    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "O Preço é obrigatório")]
    public double? Price { get; set; }
}