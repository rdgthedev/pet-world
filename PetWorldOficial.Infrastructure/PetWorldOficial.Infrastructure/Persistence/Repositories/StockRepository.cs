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
            context.Stocks.Update(stock);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Stock?> GetByProductId(int productId, CancellationToken cancellationToken)
        {
            return await context.Stocks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);
        }
    }
}