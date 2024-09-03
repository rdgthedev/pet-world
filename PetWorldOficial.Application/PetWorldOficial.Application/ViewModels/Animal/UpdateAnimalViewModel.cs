using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.Animal;

public class UpdateAnimalViewModel
{
    [Required] public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A espécie é obrigatória!")]
    public string Species { get; set; } = string.Empty;

    [Required(ErrorMessage = "A raça é obrigatória!")]
    public string Race { get; set; } = string.Empty;

    [Required(ErrorMessage = "O gênero é obrigatório!")]
    public string Gender { get; set; } = string.Empty;

    [Required] public int UserId { get; set; }
}