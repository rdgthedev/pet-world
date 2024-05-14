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

    public string PathBind(string path1, string path2) => Path.Combine(path1, path2);

    public bool DirectoryValidator(string directory) => Directory.Exists(directory);

    public async Task SaveImage(IFormFile file, string path, string imageUrl)
    {
        var filePath = Path.Combine(path, "Images");
        
        if (!DirectoryValidator(filePath)) throw new NotFoundException("Este diretório não existe ou está incorreto!");
        
        await using var imageFileStream = new FileStream(Path.Combine(filePath, imageUrl), FileMode.Create);
        await file.CopyToAsync(imageFileStream);
    }

    public string GenerateImageName(IFormFile file, string wwwrootPath)
    {
        var extension = ExtensionValidator(file.FileName);
        var imageName = $"{Guid.NewGuid()}{extension}";
        return imageName;
    }
}