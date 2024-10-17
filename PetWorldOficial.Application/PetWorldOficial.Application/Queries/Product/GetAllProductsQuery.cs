using MediatR;
using PetWorldOficial.Application.ViewModels.Product;

namespace PetWorldOficial.Application.Queries.Product;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDetailsViewModel>>
{
}