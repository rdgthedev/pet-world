using PetWorldOficial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Data.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories
{
    public class StockRepository(AppDbContext context) : IStockRepository
    {
        public async Task CreateAsync(Stock stock, CancellationToken cancellationToken)
        {
            await context.Stocks.AddAsync(stock, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Stock stock, CancellationToken cancellationToken)
        {
            var stockTracked = context.ChangeTracker
                .Entries<Stock>()
                .FirstOrDefault(e => e.Entity.Id == stock.Id);

            if (stockTracked != null)
            {
                context.Entry(stockTracked.Entity).State = EntityState.Detached;
            }
            
            var productTracked = context.ChangeTracker
                .Entries<Product>()
                .FirstOrDefault(e => e.Entity.Id == stock.ProductId);

            if (productTracked != null)
            {
                context.Entry(productTracked.Entity).State = EntityState.Detached;
            }
            
            context.Stocks.Update(stock);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Stock?> GetByProductId(int productId, CancellationToken cancellationToken)
        {
            return await context
                .Stocks
                .AsNoTracking()
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);
        }
    }
}