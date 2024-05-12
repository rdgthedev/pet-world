using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace PetWorldOficial.Domain.Interfaces.ApplicationServices;

public interface IImageService
{
    string ExtensionValidator(string extension);
    string PathBind(string path1, string path2);
    bool DirectoryValidator(string directory);
    Task CreateImage(IFormFile file, string path);
    Task<string> ProcessImage(IFormFile file, string wwwrootPath);
}