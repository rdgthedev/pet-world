using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Service;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Service
{
    public class DeleteServiceCommandHandler(
        IServiceService serviceService) : IRequestHandler<DeleteServiceCommand, DeleteServiceCommand>
    {
        public async Task<DeleteServiceCommand> Handle(
            DeleteServiceCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                ServiceDetailsViewModel? serviceResult;

                if (string.IsNullOrEmpty(request.Name))
                {
                    serviceResult = await serviceService.GetById(request.Id, cancellationToken);

                    if (serviceResult is null)
                        throw new ServiceNotFoundException("Serviço não encontrado!");

                    request.Name = serviceResult.Name;
                    request.ImageUrl = serviceResult.ImageUrl;
                    request.Price = serviceResult.Price;

                    return request;
                }

                await serviceService.Delete(request, cancellationToken);

                request.Message = "Serviço deletado com sucesso!";

                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}