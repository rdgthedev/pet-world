using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAllAsync();
    Task<Supplier?> GetByIdAsync(int id);
    Task CreateAsync(Supplier supplierModel);
    Task Update(Supplier supplierModel);
    Task Delete(Supplier supplierModel);
}