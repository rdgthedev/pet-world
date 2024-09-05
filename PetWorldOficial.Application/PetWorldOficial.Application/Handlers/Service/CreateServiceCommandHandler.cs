using MediatR;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Services.Implementations;
using PetWorldOficial.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Handlers.Service
{
    public class CreateServiceCommandHandler(
        ServiceService serviceService,
        ImageService imageService) : IRequestHandler<CreateServiceCommand, Unit>
    {
        public async Task<Unit> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await serviceService.GetByName(request.Name, cancellationToken);

            if (service != null)
                throw new ServiceAlreadyExistsException("Serviço já existe!");

            var path = Path.Combine(request.BaseUrl, "wwwroot");
            var imageUrl = imageService.GenerateImageName(request.File, path);
            await imageService.SaveImage(request.File, path, imageUrl);
        }
    }
}
