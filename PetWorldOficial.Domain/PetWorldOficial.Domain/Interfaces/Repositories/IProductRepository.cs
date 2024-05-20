using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task CreateAsync(Product product);
}