using Microsoft.AspNetCore.Http;

namespace PetWorldOficial.Application.DTOs.Service;

public class CreateServiceDTO
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string? ImageUrl { get; set; }
}