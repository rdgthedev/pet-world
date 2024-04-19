using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task CreateAsync(TEntity userModel);
    Task UpdateAsync(int id);
    Task DeleteAsync(int id);
}