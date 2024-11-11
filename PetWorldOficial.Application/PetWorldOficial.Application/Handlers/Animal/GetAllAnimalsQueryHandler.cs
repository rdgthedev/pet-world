using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.Queries.Animal;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Animal;

public class GetAllAnimalsQueryHandler(
    IAnimalService animalService,
    IUserService userService) : IRequestHandler<GetAllAnimalsQuery, IEnumerable<AnimalDetailsViewModel>>
{
    public async Task<IEnumerable<AnimalDetailsViewModel>> Handle(GetAllAnimalsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var email = request.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var user = await userService.GetByEmailAsync(
                email!,
                cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

            var animals = Enumerable.Empty<AnimalDetailsViewModel?>();

            if (request.User.IsInRole(ERole.Admin.ToString()))
            {
                animals = await animalService.GetAll(cancellationToken);

                if (!animals.Any() || animals is null)
                    throw new NotFoundException("Nenhum pet encontrado!");

                return animals!;
            }

            if (request.User.IsInRole(ERole.User.ToString()))
            {
                animals = await animalService.GetByOwnerId(user.Id, cancellationToken);

                if (!animals.Any() || animals is null)
                    throw new NotFoundException("Nenhum pet encontrado!");

                return animals!;
            }

            throw new UnauthorizedUserException("Acesso não autorizado");
        }
        catch (Exception)
        {
            throw;
        }
    }
}