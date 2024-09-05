using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Commands.Service
{
    public record CreateServiceCommand : IRequest<Unit>
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Preço é obrigatório")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "A imagem é obrigatória")]
        public IFormFile File { get; set; }
        public string BaseUrl { get; set; } = string.Empty;
    }
}
