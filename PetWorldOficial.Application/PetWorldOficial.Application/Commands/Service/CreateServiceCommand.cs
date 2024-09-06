﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Commands.Service
{
    public record CreateServiceCommand : IRequest<CreateServiceCommand>
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Preço é obrigatório")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "A imagem é obrigatória")]
        public IFormFile File { get; set; } = null!;
        public string BaseUrl { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
