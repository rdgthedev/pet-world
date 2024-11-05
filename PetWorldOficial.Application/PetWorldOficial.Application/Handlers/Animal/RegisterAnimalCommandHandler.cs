using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Services.Implementations;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Race;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using System.Security.Claims;

namespace PetWorldOficial.Application.Handlers.Animal;

public class RegisterAnimalCommandHandler(
    IUserService userService,
    IAnimalService animalService,
    IRaceService raceService,
    ICategoryService categoryService,
    IMemoryCache memoryCache,
    IImageService imageService) : IRequestHandler<RegisterAnimalCommand, RegisterAnimalCommand>
{
    public async Task<RegisterAnimalCommand> Handle(
        RegisterAnimalCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name.Trim()))
            {
                var email = request.UserPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                var user = await userService.GetByEmailAsync(
                    email!,
                    cancellationToken);

                if (user is null)
                    throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

                request.Categories = await memoryCache.GetOrCreateAsync("Categories", async entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddHours(8);
                    var categories = await categoryService.GetAll(cancellationToken);
                    return categories;
                }) ?? Enumerable.Empty<CategoryDetailsViewModel>();

                request.Races = await memoryCache.GetOrCreateAsync("Races", async entry =>
                {
                    entry.AbsoluteExpiration = DateTime.Now.AddHours(8);
                    var races = await raceService.GetAll(cancellationToken);
                    return races;
                }) ?? Enumerable.Empty<RaceDetailsViewModel>();

                if (request.UserPrincipal!.IsInRole(ERole.Admin.ToString())
                    && string.IsNullOrEmpty(request.Name.Trim()))
                {
                    request.Users = await userService.GetAllUsersExceptCurrentAsync(user.Id, cancellationToken);
                    request.AdminId = user.Id;
                    return request;
                }

                if (request.UserPrincipal.IsInRole(ERole.User.ToString())
                    && request.UserId == default)
                {
                    request.UserId = user.Id;
                    return request;
                }
            }

            if (request.File != null)
            {
                var path = Path.Combine(request.BaseUrl, "wwwroot");

                request.ImageUrl = imageService.GenerateImageName(request.File, path);
                await imageService.SaveImage(request.File, path, request.ImageUrl);
            }

            await animalService.Create(request, cancellationToken);

            request.Message = "Pet cadastrado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            // request.Message = "Ocorreu um erro interno. Não foi possível realizar o cadastro do pet!";
            throw;
        }
    }
}