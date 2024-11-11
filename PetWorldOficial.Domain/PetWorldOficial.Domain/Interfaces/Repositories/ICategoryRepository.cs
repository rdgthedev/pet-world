using PetWorldOficial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category, CancellationToken cancellationToken);
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task<Category?> GetByType(ECategoryType type, CancellationToken cancellationToken);
        Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Category>> GetAllServiceCategories(CancellationToken cancellationToken);
        Task<IEnumerable<Category>> GetAllProductCategories(CancellationToken cancellationToken);
        Task<IEnumerable<Category>> GetAllAnimalCategories(CancellationToken cancellationToken);
        Task UpdateAsync(Category category, CancellationToken cancellationToken);
        Task DeleteAsync(Category category, CancellationToken cancellationToken);
    }
}