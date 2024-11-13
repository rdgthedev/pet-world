using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Queries.User;

public record GetAllClientsAndEmployeesQuery(ClaimsPrincipal UserPrincipal)
    : IRequest<IEnumerable<UserDetailsViewModel>>
{
}