
using PetWorldOficial.Domain.Entities;

public interface IStockRepository
{
    Task CreateAsync(Stock stock, CancellationToken cancellationToken);
    Task UpdateAsync(Stock stock, CancellationToken cancellationToken);
}