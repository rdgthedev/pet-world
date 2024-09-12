using MediatR;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Services.Implementations;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Animal;

public class DeleteAnimalCommandHandler(
    IUserService userService,
    IAnimalService animalService,
    IRaceService raceService,
    ICategoryService categoryService) : IRequestHandler<DeleteAnimalCommand, DeleteAnimalCommand>
{
    public async Task<DeleteAnimalCommand> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                var user = await userService.GetByUserNameAsync(
                    request.UserPrincipal?.Identity?.Name!, cancellationToken);

                if (user is null)
                    throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

                var animal = await animalService.GetByIdWithOwnerAndCategoryAndRaceAsync(request.Id, cancellationToken);

                if (animal is null)
                    throw new AnimalNotFoundException("Pet não encontrado!");

                request.Name = animal.Name;
                request.CategoryName = animal.Category.Title;
                request.RaceName = animal.Race.Name;
                request.UserId = animal.OwnerId;
                request.Gender = animal.Gender;
                request.RaceId = animal.Race.Id;
                request.CategoryId = animal.Category.Id;

                return request;
            }

            await animalService.Delete(request, cancellationToken);
            request.Message = "Pet deletado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}