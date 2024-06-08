using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}