using MediatR;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Queries.User;

public record GetUserByIdQuery(int Id) : IRequest<UserDetailsViewModel>
{
    
}