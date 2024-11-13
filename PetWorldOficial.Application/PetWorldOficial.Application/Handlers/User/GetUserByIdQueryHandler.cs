using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Application.Queries.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.User;

public class GetUserByIdQueryHandler(
    IUserService userService,
    UserManager<Domain.Entities.User> userManager,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDetailsViewModel>
{
    public async Task<UserDetailsViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userResult = await userService.GetByIdAsync(request.Id, cancellationToken);
            userResult.RoleName = (await userManager.GetRolesAsync(mapper.Map<Domain.Entities.User>(userResult))).FirstOrDefault();

            return userResult;
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