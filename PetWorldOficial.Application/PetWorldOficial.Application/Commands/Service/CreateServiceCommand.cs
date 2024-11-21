using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using PetWorldOficial.Application.ViewModels;

namespace PetWorldOficial.Application.Commands.Service
{
    public record CreateServiceCommand : IRequest<CreateServiceCommand>
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A imagem é obrigatória!")]
        public IFormFile File { get; set; } = null!;

        [Required(ErrorMessage = "A duração em minutos é obrigatória!")]
        public int? DurationInMinutes { get; set; }

        [Required(ErrorMessage = "O Preço é obrigatório!")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        public int? CategoryId { get; set; }


        public string BaseUrl { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public IEnumerable<CategoryDetailsViewModel> Categories { get; set; } =
            Enumerable.Empty<CategoryDetailsViewModel>();
    }
}