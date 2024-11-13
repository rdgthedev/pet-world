using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Service
{
    public class CreateServiceCommandHandler(
        IServiceService serviceService,
        IImageService imageService,
        IMemoryCache memoryCache,
        ICategoryService categoryService) : IRequestHandler<CreateServiceCommand, CreateServiceCommand>
    {
        public async Task<CreateServiceCommand> Handle(
            CreateServiceCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name.Trim()))
                {
                    if (!memoryCache.TryGetValue("ServiceCategories", out IEnumerable<CategoryDetailsViewModel>? categories))
                    {
                        categories = await categoryService.GetAllServiceCategories(cancellationToken);
                        memoryCache.Set("ServiceCategories", categories, TimeSpan.FromHours(8));
                    }

                    request.Categories = categories;
                    return request;
                }

                var serviceResult = await serviceService.GetByName(request.Name, cancellationToken);

                if (serviceResult != null)
                    throw new ServiceAlreadyExistsException("Serviço já existe!");

                var path = Path.Combine(request.BaseUrl, "wwwroot");

                var imageUrl = imageService.GenerateImageName(request.File, path);
                await imageService.SaveImage(request.File, path, imageUrl);

                request.ImageUrl = imageUrl;

                await serviceService.Create(request, cancellationToken);

                request.Message = "Serviço cadastrado com sucesso!";

                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}