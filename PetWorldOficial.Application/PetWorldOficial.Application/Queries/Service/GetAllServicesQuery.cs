using MediatR;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Queries.Service;

public record GetAllServicesQuery : IRequest<IEnumerable<ServiceDetailsViewModel>>
{
    
}