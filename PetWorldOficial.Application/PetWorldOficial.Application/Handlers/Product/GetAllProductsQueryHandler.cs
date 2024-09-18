using MediatR;
using PetWorldOficial.Application.Queries.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Product;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Product;

public class GetAllProductsQueryHandler(
    IProductService productService) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDetailsViewModel>>
{
    public async Task<IEnumerable<ProductDetailsViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await productService.GetAll(cancellationToken);

            if (!products.Any() || products is null)
                throw new ProductNotFoundException("Nenhum produto encontrado!");

            return products;
        }
        catch (Exception)
        {
            throw;
        }
    }
}