using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<Service?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task CreateAsync(Service service, CancellationToken cancellationToken);
    Task<IEnumerable<Service>> GetAllAsync(CancellationToken cancellationToken);
    Task<Service?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Service service, CancellationToken cancellationToken);
    Task DeleteAsync(Service service, CancellationToken cancellationToken);
}