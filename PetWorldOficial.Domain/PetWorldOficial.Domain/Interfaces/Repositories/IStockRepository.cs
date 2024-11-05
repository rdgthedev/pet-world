
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IStockRepository
{
    Task CreateAsync(Stock stock, CancellationToken cancellationToken);
    Task UpdateAsync(Stock stock, CancellationToken cancellationToken);
}