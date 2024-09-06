using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Services.Implementations;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Handlers.Service
{
    public class DeleteServiceCommandHandler(
        IServiceService serviceService) : IRequestHandler<DeleteServiceCommand, DeleteServiceCommand>
    {
        public async Task<DeleteServiceCommand> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceResult = await serviceService.GetById(request.Id, cancellationToken);

                if (serviceResult is null)
                    throw new ServiceNotFoundException("Serviço não encontrado!");

                request.ImageUrl = serviceResult.ImageUrl;

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
