using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IAnimalRepository
{
    Task<IEnumerable<Animal?>> GetAllAsync(CancellationToken cancellationToken);
    Task<Animal?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Animal animal, CancellationToken cancellationToken);
    Task DeleteAsync(Animal animal, CancellationToken cancellationToken);
    Task CreateAsync(Animal animal, CancellationToken cancellationToken);
    Task<IEnumerable<Animal?>> GetByUserIdAsync(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<Animal?>> GetWithUser(CancellationToken cancellationToken);
}