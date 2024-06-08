using Microsoft.AspNetCore.Http;

namespace PetWorldOficial.Application.DTOs.Service;

public record CreateServiceDTO(string Name, double Price, string? ImageUrl);