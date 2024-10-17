using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.ViewModels.Product;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDetailsViewModel>> GetAll(CancellationToken cancellationToken);
    Task<ProductDetailsViewModel> GetById(int id, CancellationToken cancellationToken);
    Task<ProductDetailsViewModel> GetByName(string productName, CancellationToken cancellationToken);
    Task Create(CreateProductCommand product, CancellationToken cancellationToken);
    Task Update(UpdateProductCommand product, CancellationToken cancellationToken);
    Task Delete(DeleteProductCommand product, CancellationToken cancellationToken);
}