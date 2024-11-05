using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Data.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context
                .Categories
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetByType(
            ECategoryType categoryType,
            CancellationToken cancellationToken)
        {
            return await context
                .Categories
                .AsNoTracking()
                .Where(s => s.Type == categoryType.ToString())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetAllServiceCategories(CancellationToken cancellationToken)
            => await context
                .Categories
                .AsNoTracking()
                .Where(c => c.Type == ECategoryType.Service.ToString())
                .ToListAsync(cancellationToken);
        
        public async Task<IEnumerable<Category>> GetAllProductCategories(CancellationToken cancellationToken)
            => await context
                .Categories
                .AsNoTracking()
                .Where(c => c.Type == ECategoryType.Product.ToString())
                .ToListAsync(cancellationToken);

        public async Task CreateAsync(Category category, CancellationToken cancellationToken)
        {
            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}