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
    public record UpdateServiceCommand : IRequest<UpdateServiceCommand>
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "O Preço é obrigatório")]
        public double? Price { get; set; }
        public string Message { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
        public string RootPath { get; set; } = string.Empty;
    }
}