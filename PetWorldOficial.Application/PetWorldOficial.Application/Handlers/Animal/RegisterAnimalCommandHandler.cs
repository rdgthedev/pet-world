using MediatR;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Animal;

public class RegisterAnimalCommandHandler(
    IUserService userService,
    IAnimalService animalService) : IRequestHandler<RegisterAnimalCommand, RegisterAnimalCommand>
{
    public async Task<RegisterAnimalCommand> Handle(RegisterAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetByUserNameAsync(request.UserPrincipal.Identity?.Name!, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");


            if (request.UserPrincipal.IsInRole(ERole.Admin.ToString()) && !request.Users.Any())
            {
                request.Users = await userService.GetAllAsync(cancellationToken);
                return request;
            }

            if (request.UserPrincipal.IsInRole(ERole.User.ToString()) && request.UserId == default)
            {
                request.UserId = user.Id;
                return request;
            }

            var animals = await animalService.GetByUserId(user.Id, cancellationToken);

            await (animals.Any(a => a!.Name == request.Name)
                ? throw new AnimalAlreadyExistsException("Este pet já está cadastrado!")
                : animalService.Create(request, cancellationToken));

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