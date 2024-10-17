using MediatR;
using PetWorldOficial.Application.Queries.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.User;

public class GetAllClientsAndEmployesQueryHandler(
    IUserService userService) : IRequestHandler<GetAllUsersExceptCurrentQuery, IEnumerable<UserDetailsViewModel>>
{
    public async Task<IEnumerable<UserDetailsViewModel>> Handle(
        GetAllUsersExceptCurrentQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetByUserNameAsync(request.UserPrincipal.Identity?.Name!, cancellationToken);

            if (user is null)
                throw new AccessFailureException("Falha ao acessar. Tente recarregar a página ou fazer o Login novamente!");

            var users = await userService.GetAllUsersExceptCurrentAsync(user.Id, cancellationToken);

            if (!users.Any())
                throw new UserNotFoundException("Nenhum usuário encontrado!");

            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }
}