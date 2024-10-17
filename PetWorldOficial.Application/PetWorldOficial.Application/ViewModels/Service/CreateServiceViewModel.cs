using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PetWorldOficial.Application.ViewModels.Service;

public class CreateServiceViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O Preço é obrigatório")]
    public double? Price { get; set; }

    [Required(ErrorMessage = "A imagem é obrigatória")]
    public IFormFile File { get; set; }
}