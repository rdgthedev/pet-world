using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface ISupplierRepository
{
    Task CreateAsync(Supplier supplier, CancellationToken cancellationToken);
    Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken);
    Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken);
    Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken);
}