using MediatR;
using PetWorldOficial.Application.Queries.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Service;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Service;

public class GetAllServicesQueryHandler(
    IServiceService serviceService) : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceDetailsViewModel>>
{
    public async Task<IEnumerable<ServiceDetailsViewModel>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
		try
		{
			var services = await serviceService.GetAll(cancellationToken);

			if (!services.Any() || services is null)
				throw new ServiceNotFoundException("Nenhum serviço encontrado!");

			return services;
		}
		catch (Exception)
		{

			throw;
		}
    }
}