using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace PetWorldOficial.Application.ViewModels.Animal;

public class UpdateAnimalViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "A espécie é obrigatória!")]
    public string Species { get; set; }
    
    [Required(ErrorMessage = "A raça é obrigatória!")]
    public string Race { get; set; }
    
    [Required(ErrorMessage = "O gênero é obrigatório!")]
    public string Gender { get; set; }
    
    [Required]
    public int UserId { get; set; }
}