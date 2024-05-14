using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAll();
    Task<Service?> GetById(int id);
    Task Create(Service service);
    Task Update(Service service);
    Task Delete(Service service);
}