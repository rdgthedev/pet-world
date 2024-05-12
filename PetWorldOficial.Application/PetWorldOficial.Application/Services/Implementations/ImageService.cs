using Microsoft.AspNetCore.Http;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.ApplicationServices;

namespace PetWorldOficial.Application.Services.Implementations;

public class ImageService : IImageService
{
    public string ExtensionValidator(string fileName)
    {
        HashSet<string> extensions = [".jpg", ".png", ".jpeg"];
        var imageExtension = Path.GetExtension(fileName);
        
        if (string.IsNullOrEmpty(extensions.FirstOrDefault(extension => extension.Equals(imageExtension))))
            throw new Exception("Não foi possível processar esta imagem! Certifique de que a imagem é do tipo " +
                                ".jpg\", \".png\", \".jpeg");
        
        return imageExtension;
    }

    public string PathBind(string path1, string path2)
    {
        var a = Path.Combine(path1, "Images");
        return Path.Combine(path1, path2);
    }

    public bool DirectoryValidator(string directory) => Directory.Exists(directory);

    public async Task CreateImage(IFormFile file, string path)
    {
        await using var imageFileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(imageFileStream);
    }

    public async Task<string> ProcessImage(IFormFile file, string wwwrootPath)
    {
        var extension = ExtensionValidator(file.FileName);
        var filePath = PathBind(wwwrootPath, "Images");

        if (!DirectoryValidator(filePath))
            throw new NotFoundException("Este diretório não existe ou está incorreto!");
        
        var newFilePath = PathBind(filePath, $"{Guid.NewGuid()}{extension}");
        await CreateImage(file, newFilePath);
        return newFilePath;
    }
}