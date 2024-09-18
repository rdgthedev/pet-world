using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Race;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Animal;

public class RegisterAnimalCommandHandler(
    IUserService userService,
    IAnimalService animalService,
    IRaceService raceService,
    ICategoryService categoryService,
    IMemoryCache memoryCache) : IRequestHandler<RegisterAnimalCommand, RegisterAnimalCommand>
{
    public async Task<RegisterAnimalCommand> Handle(
        RegisterAnimalCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name.Trim()))
            {
                var user = await userService.GetByUserNameAsync(request.UserPrincipal?.Identity?.Name!,
                    cancellationToken);

                if (user is null)
                    throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

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

                request.Races = races;
                request.Categories = categories;

                if (request.UserPrincipal!.IsInRole(ERole.Admin.ToString())
                    && string.IsNullOrEmpty(request.Name.Trim()))
                {
                    request.Users = await userService.GetAllAsync(cancellationToken);
                    return request;
                }

                if (request.UserPrincipal.IsInRole(ERole.User.ToString())
                    && request.UserId == default)
                {
                    request.UserId = user.Id;
                    return request;
                }
            }

            await animalService.Create(request, cancellationToken);
            request.Message = "Pet cadastrado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            request.Message = "Ocorreu um erro interno. Não foi possível realizar o cadastro do pet!";
            throw;
        }
    }
}