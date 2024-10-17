using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.ViewModels.Animal;

namespace PetWorldOficial.Application.Queries.Animal;

public record GetAllAnimalsQuery(ClaimsPrincipal User) : IRequest<IEnumerable<AnimalDetailsViewModel>>
{
    
}