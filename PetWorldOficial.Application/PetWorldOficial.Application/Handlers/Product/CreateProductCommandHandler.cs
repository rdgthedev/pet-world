using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Supplier;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Product;

public class CreateProductCommandHandler(
    IProductService productService,
    ISupplierService supplierService,
    IImageService imageService,
    ICategoryService categoryService,
    IStockService stockService) : IRequestHandler<CreateProductCommand, CreateProductCommand>
{
    public async Task<CreateProductCommand> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name.Trim()))
            {
                var categories = await categoryService.GetAllProductCategories(cancellationToken);

                if (!categories.Any())
                    throw new Exception("Não é possível cadastrar um produto, pois não há categorias cadastradas!");

                var suppliers = await supplierService.GetAllAsync(cancellationToken);

                if (!suppliers.Any())
                    throw new SupplierNotFoundException(
                        "Não é possível cadastrar um produto, pois não há fornecedores cadastrados!");

                request.Categories = categories;
                request.Suppliers = suppliers;
                return request;
            }

            var product = await productService.GetByName(request.Name, cancellationToken);

            if (product != null)
                throw new ProductAlreadyExistsException("Produto já existe!");

            var path = Path.Combine(request.BaseUrl, "wwwroot");

            request.ImageUrl = imageService.GenerateImageName(request.File, path);
            await imageService.SaveImage(request.File, path, request.ImageUrl);

            await productService.Create(request, cancellationToken);

            product = await productService.GetByName(request.Name, cancellationToken);

            if (product != null)
            {
                request.ProductId = product.Id;
                await stockService.CreateAsync(request, cancellationToken);
            }

            request.Message = "Produto criado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}