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
            var user = await userService.GetByUserNameAsync(request.User.Identity?.Name!, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

            var role = request.User.IsInRole(ERole.Admin.ToString())
                ? ERole.Admin
                : ERole.User;

            var animals = Enumerable.Empty<AnimalDetailsViewModel?>();

            if (request.User.IsInRole(ERole.Admin.ToString()))
            {
                animals = await animalService.GetWithOwnerAndRace(cancellationToken);

                if (!animals.Any() || animals is null)
                    throw new NotFoundException("Nenhum pet encontrado!");

                return animals!;
            }

            if (request.User.IsInRole(ERole.User.ToString()))
            {
                animals = await animalService.GetByUserIdWithOwnerAndRace(user.Id, cancellationToken);

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