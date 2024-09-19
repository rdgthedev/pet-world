using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories
{
    public class StockRepository(AppDbContext context) : IStockRepository
    {
        public async Task CreateAsync(Stock stock, CancellationToken cancellationToken)
        {
            await context.Stocks.AddAsync(stock, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
