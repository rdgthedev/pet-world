using MediatR;
using PetWorldOficial.Application.DTOs.Service.Output;
using PetWorldOficial.Application.Queries.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Service;
using PetWorldOficial.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Handlers.Service
{
    public class GetServiceByIdQueryHandler(
        IServiceService serviceService) : IRequestHandler<GetServiceByIdQuery, ServiceDetailsViewModel>
    {
        public async Task<ServiceDetailsViewModel> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var service = await serviceService.GetById(request.Id, cancellationToken);

                if (service is null)
                    throw new ServiceNotFoundException("Serviço não encontrado!");

                return service;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
