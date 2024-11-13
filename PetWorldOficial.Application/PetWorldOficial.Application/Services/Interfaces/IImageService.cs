using Microsoft.AspNetCore.Http;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IImageService
{
    string ExtensionValidator(string extension);
    bool DirectoryValidator(string directory);
    Task SaveImage(IFormFile file, string path, string imageUrl);
    string GenerateImageName(IFormFile file, string wwwrootPath);
}