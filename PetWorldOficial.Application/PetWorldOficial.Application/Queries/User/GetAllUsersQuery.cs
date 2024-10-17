using MediatR;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Queries.User;

public record GetAllUsersQuery : IRequest<IEnumerable<UserDetailsViewModel>>
{
    
}