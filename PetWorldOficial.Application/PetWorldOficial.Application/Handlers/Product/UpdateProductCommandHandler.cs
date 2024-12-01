using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Product;
using PetWorldOficial.Application.ViewModels.Supplier;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Product;

public class UpdateProductCommandHandler(
    IProductService productService,
    ICategoryService categoryService,
    ISupplierService supplierService,
    IImageService imageService,
    IStockService stockService,
    IMemoryCache memoryCache) : IRequestHandler<UpdateProductCommand, UpdateProductCommand>
{
    public async Task<UpdateProductCommand> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name.Trim()))
            {
                var product = await productService.GetById(request.Id, cancellationToken);

                if (product is null)
                    throw new ProductNotFoundException("Produto não encontrado!");

                request.Name = product.Name;
                request.Description = product.Description;
                request.Price = product.Price;
                request.SupplierId = product.SupplierId;
                request.CategoryId = product.CategoryId;
                request.ImageUrl = product.ImageUrl;
                request.QuantityInStock = product.Stock.Quantity;
                request.Categories = await categoryService.GetAllProductCategories(cancellationToken);
                request.Suppliers = await supplierService.GetAllAsync(cancellationToken);
                request.CategoryName = product.CategoryName;
                request.SupplierName = product.SupplierName;
                request.ProductId = product.Id;

                return request;
            }

            if (request.File is not null)
            {
                var path = Path.Combine(request.BaseUrl, "wwwroot");

                request.ImageUrl = imageService.GenerateImageName(request.File, path);
                await imageService.SaveImage(request.File, path, request.ImageUrl);
            }

            await productService.Update(request, cancellationToken);

            var productResult = await productService.GetById(request.Id, cancellationToken);

            if (productResult is null)
                throw new ProductNotFoundException("Produto não encontrado!");

            if (request.QuantityInStock != productResult.Stock.Quantity)
            {
                request.Id = productResult.Stock.Id;
                await stockService.UdpateAsync(request, cancellationToken);
            }

            request.Message = "Produto atualizado com sucesso!";

            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}