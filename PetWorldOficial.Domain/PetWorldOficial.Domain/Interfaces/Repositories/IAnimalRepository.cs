using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IAnimalRepository : IBaseRepository<Animal>
{
    Task CreateAsync(Animal entity);
    Task<IEnumerable<Animal?>> GetByUserIdAsync(int id);
    public Task<IEnumerable<Animal?>> GetWithUser();
}