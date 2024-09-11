using MediatR;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Animal;

public class DeleteAnimalCommandHandler(
    IUserService userService,
    IAnimalService animalService) : IRequestHandler<DeleteAnimalCommand, DeleteAnimalCommand>
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

                var animal = await animalService.GetById(request.Id, cancellationToken);

                if (animal is null)
                    throw new AnimalNotFoundException("Pet não encontrado!");

                request.Name = animal.Name;
                request.Category = animal.Category;
                request.Race = animal.Race;
                request.Gender = animal.Gender;
                
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