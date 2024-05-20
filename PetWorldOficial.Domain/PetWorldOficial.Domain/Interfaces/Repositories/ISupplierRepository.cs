using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task CreateAsync(Supplier product);
}