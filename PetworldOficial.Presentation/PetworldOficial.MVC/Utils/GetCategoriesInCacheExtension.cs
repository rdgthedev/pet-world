using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.ViewModels;

namespace PetworldOficial.MVC.Utils;

public static class GetCategoriesInCacheExtension
{
    public static IEnumerable<CategoryDetailsViewModel> GetCategories(
        this CreateServiceCommand command,
        IMemoryCache cache)
    {
        if (cache.TryGetValue("Categories", out IEnumerable<CategoryDetailsViewModel>? categories))
            command.Categories = categories!;

        return command.Categories;
    }
}