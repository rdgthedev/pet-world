using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace PetWorldOficial.Domain.Interfaces.ApplicationServices;

public interface IImageService
{
    string ExtensionValidator(string extension);
    bool DirectoryValidator(string directory);
    Task SaveImage(IFormFile file, string path, string imageUrl);
    string GenerateImageName(IFormFile file, string wwwrootPath);
}