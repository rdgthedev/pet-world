using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Race;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Animal;

public class UpdateAnimalCommandHandler(
    IUserService userService,
    IAnimalService animalService,
    ICategoryService categoryService,
    IRaceService raceService,
    IMemoryCache memoryCache,
    IImageService imageService) : IRequestHandler<UpdateAnimalCommand, UpdateAnimalCommand>
{
    public async Task<UpdateAnimalCommand> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                var email = request.UserPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                var user = await userService.GetByEmailAsync(
                    email!,
                    cancellationToken);

                if (user is null)
                    throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

                var animal = await animalService.GetByIdWithOwnerAndCategoryAndRaceAsync(request.Id, cancellationToken);

                if (animal is null)
                    throw new AnimalNotFoundException("Pet não encontrado!");

                if (!memoryCache.TryGetValue("Categories", out IEnumerable<CategoryDetailsViewModel>? categories))
                {
                    categories = await categoryService.GetAll(cancellationToken);
                    memoryCache.Set("Categories", categories, TimeSpan.FromHours(8));
                }

                if (!memoryCache.TryGetValue("Races", out IEnumerable<RaceDetailsViewModel>? races))
                {
                    races = await raceService.GetAll(cancellationToken);
                    memoryCache.Set("Races", races, TimeSpan.FromHours(8));
                }

                request.Name = animal.Name;
                request.Gender = animal.Gender;
                request.RaceName = animal.Race.Name;
                request.RaceId = animal.Race.Id;
                request.CategoryId = animal.Category.Id;
                request.CategoryTitle = animal.Category.Title;
                request.BirthDate = animal.BirthDate;
                request.UserId = user.Id;
                request.Categories = categories;
                request.Races = races;
                request.ImageUrl = animal.ImageUrl;

                return request;
            }

            if (request.File != null)
            {
                var path = Path.Combine(request.BaseUrl, "wwwroot");

                request.ImageUrl = imageService.GenerateImageName(request.File, path);
                await imageService.SaveImage(request.File, path, request.ImageUrl);
            }

            await animalService.Update(request, cancellationToken);

            request.Message = "Pet alterado com sucesso!";

            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}