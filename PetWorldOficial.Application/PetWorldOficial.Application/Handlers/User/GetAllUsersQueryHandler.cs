using MediatR;
using PetWorldOficial.Application.Queries.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.User;

public class GetAllUsersQueryHandler(IUserService userService)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDetailsViewModel>>
{
    public async Task<IEnumerable<UserDetailsViewModel>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await userService.GetAllAsync(cancellationToken)
                ?? throw new UserNotFoundException("Nenhum usuário encontrado!");
        }
        catch (Exception e)
        {
            throw;
        }
    }
}