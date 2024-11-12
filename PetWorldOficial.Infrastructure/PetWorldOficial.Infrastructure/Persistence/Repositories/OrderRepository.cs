using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Data.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class OrderRepository(
    AppDbContext context) : IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Order order)
    {
        throw new NotImplementedException();
    }
}