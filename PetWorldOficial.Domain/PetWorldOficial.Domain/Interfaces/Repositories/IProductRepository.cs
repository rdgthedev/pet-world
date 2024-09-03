using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task CreateAsync(Product product, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Product entity, CancellationToken cancellationToken);
    Task DeleteAsync(Product entity, CancellationToken cancellationToken);
}