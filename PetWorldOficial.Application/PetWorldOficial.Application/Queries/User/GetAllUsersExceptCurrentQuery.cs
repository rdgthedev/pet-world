using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Queries.User;

public record GetAllUsersExceptCurrentQuery(ClaimsPrincipal UserPrincipal)
    : IRequest<IEnumerable<UserDetailsViewModel>>
{
}