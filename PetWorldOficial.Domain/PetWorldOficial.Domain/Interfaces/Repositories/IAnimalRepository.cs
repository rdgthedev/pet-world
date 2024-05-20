using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IAnimalRepository : IBaseRepository<Animal>
{
    Task CreateAsync(Animal entity);
}