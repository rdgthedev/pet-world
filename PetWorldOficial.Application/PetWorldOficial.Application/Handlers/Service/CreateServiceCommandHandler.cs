using MediatR;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Services.Implementations;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Handlers.Service
{
    public class CreateServiceCommandHandler(
        IServiceService serviceService,
        IImageService imageService) : IRequestHandler<CreateServiceCommand, CreateServiceCommand>
    {
        public async Task<CreateServiceCommand> Handle(
            CreateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceResult = await serviceService.GetByName(request.Name, cancellationToken);

                if (serviceResult != null)
                    throw new ServiceAlreadyExistsException("Serviço já existe!");

                var path = Path.Combine(request.BaseUrl, "wwwroot");

                var imageUrl = imageService.GenerateImageName(request.File, path);
                await imageService.SaveImage(request.File, path, imageUrl);

                request.ImageUrl = imageUrl;

                await serviceService.Create(request, cancellationToken);

                request.Message = "Pet cadastrado com sucesso!";

                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
