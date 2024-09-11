using MediatR;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Animal;

public class UpdateAnimalCommandHandler(
    IUserService userService,
    IAnimalService animalService) : IRequestHandler<UpdateAnimalCommand, UpdateAnimalCommand>
{
    public async Task<UpdateAnimalCommand> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                var user = await userService.GetByUserNameAsync(request.UserPrincipal?.Identity?.Name!,
                    cancellationToken);

                if (user is null)
                    throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

                var animal = await animalService.GetById(request.Id, cancellationToken);

                if (animal is null)
                    throw new AnimalNotFoundException("Pet não encontrado!");

                request.Name = animal.Name;
                request.Gender = animal.Gender;
                request.Race = animal.Race;
                request.Category = animal.Category;
                request.UserId = user.Id;

                return request;
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