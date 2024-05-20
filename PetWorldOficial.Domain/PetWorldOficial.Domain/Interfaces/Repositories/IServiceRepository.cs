using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IServiceRepository : IBaseRepository<Service>
{
    Task<Service?> GetByNameAsync(string name);
    Task CreateAsync(Service product);
}