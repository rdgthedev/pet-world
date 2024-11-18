using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Data.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class OrderRepository(
    AppDbContext context) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context
            .Orders
            .AsNoTracking()
            .Include(o => o.Client)
            .Include(o => o.Items)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllByClientId(int clientId, CancellationToken cancellationToken)
    {
        return await context
            .Orders
            .AsNoTracking()
            .Include(o => o.Client)
            .Include(o => o.Items)
            .Where(o => o.ClientId == clientId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context
            .Orders
            .AsNoTracking()
            .Include(o => o.Client)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken)
    {
        // foreach (var cartItem in order.Items)
        // {
        //     var existingProduct = await context.Products
        //         .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId, cancellationToken);
        //
        //     if (existingProduct != null)
        //     {
        //         cartItem.SetProduct(existingProduct.Id, existingProduct);
        //         context.Entry(existingProduct).State = EntityState.Unchanged;
        //     }
        // }

        var orderEntity = await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return orderEntity.Entity;
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        var existingOrder = await context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == order.Id, cancellationToken);

        if (existingOrder != null)
        {
            context.Entry(existingOrder).State = EntityState.Detached;
        }

        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(Order order)
    {
        throw new NotImplementedException();
    }
}