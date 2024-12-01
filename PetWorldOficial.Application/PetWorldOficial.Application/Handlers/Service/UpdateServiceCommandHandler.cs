using MediatR;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Service
{
    public class UpdateServiceCommandHandler(
        IServiceService serviceService,
        ICategoryService categoryService,
        IImageService imageService) : IRequestHandler<UpdateServiceCommand, UpdateServiceCommand>
    {
        public async Task<UpdateServiceCommand> Handle(UpdateServiceCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                var serviceResult = await serviceService.GetById(request.Id, cancellationToken);

                if (serviceResult is null)
                    throw new ServiceNotFoundException("Serviço não encontrado!");

                var categories = await categoryService.GetAllServiceCategories(cancellationToken);

                if (!categories.Any())
                    throw new Exception("Nenhuma categoria encontrada!");

                request.Name = serviceResult.Name;
                request.ImageUrl = serviceResult.ImageUrl;
                request.Price = serviceResult.Price;
                request.DurationInMinutes = serviceResult.DurationInMinutes;
                request.Category = serviceResult.Category;
                request.CategoryId = serviceResult.Category.Id;
                request.Categories = categories;

                return request;
            }

            if (!request.CurrentName.Equals(request.Name))
            {
                var serviceExists = await serviceService.GetByName(request.Name, cancellationToken);

                if (serviceExists != null)
                    throw new ServiceAlreadyExistsException("Já existe um serviço com esse nome!");
            }

            switch (request.File)
            {
                case not null:
                    var imageUrl = imageService.GenerateImageName(request.File, request.RootPath);
                    await imageService.SaveImage(request.File, request.RootPath, imageUrl);
                    request.ImageUrl = imageUrl;
                    break;
                default: break;
            }

            await serviceService.Update(request, cancellationToken);

            request.Message = "Serviço alterado com sucesso!";

            return request;
        }
    }
}