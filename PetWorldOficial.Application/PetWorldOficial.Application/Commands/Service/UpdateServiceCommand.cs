using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Commands.Service
{
    public record UpdateServiceCommand : IRequest<UpdateServiceCommand>
    {
        [Required] public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório!")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória!")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required(ErrorMessage = "A duração é obrigatória!")]
        [RegularExpression("^(30|60)$", ErrorMessage = "A duração deve ser de 30 ou 60 minutos!")]
        public int? DurationInMinutes { get; set; }

        public string Message { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
        public string RootPath { get; set; } = string.Empty;

        public IEnumerable<CategoryDetailsViewModel> Categories { get; set; } =
            Enumerable.Empty<CategoryDetailsViewModel>();
    }
}