using MediatR;
using PetWorldOficial.Application.Queries.Service;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Handlers.Service;

public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceDetailsViewModel>>
{
    public Task<IEnumerable<ServiceDetailsViewModel>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}