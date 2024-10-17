using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.ViewModels.Supplier;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<SupplierDetailsViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(CreateProductCommand command, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateProductCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteProductCommand command, CancellationToken cancellationToken);
}