using MediatR;
using PetWorldOficial.Application.Queries.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.User;

public class GetUserByIdQueryHandler(IUserService userService) 
    : IRequestHandler<GetUserByIdQuery, UserDetailsViewModel>
{
    public async Task<UserDetailsViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await userService.GetByIdAsync(request.Id, cancellationToken);
        }
        catch (UserNotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}