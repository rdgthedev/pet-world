using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Data.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class CartItemRepository(AppDbContext context) : ICartItemRepository
{
    public async Task DeleteRangeAsync(List<CartItem> items, CancellationToken cancellationToken)
    {
        context.CartItems.RemoveRange(items);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CartItem item, CancellationToken cancellationToken)
    {
        context.CartItems.Remove(item);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<CartItem?> GetById(int id, CancellationToken cancellationToken)
    {
        return await context.CartItems
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}