using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories
{
    public class CartRepository(
        AppDbContext context) : ICartRepository
    {
        public async Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Carts.ToListAsync(cancellationToken);
        }

        public async Task<Cart> AddAsync(Cart cart, CancellationToken cancellationToken)
        {
            var cartResult = await context.Carts.AddAsync(cart, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return cartResult.Entity;
        }

        public async Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Carts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken)
        {
            context.Update(cart);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken)
        {
            context.Remove(cart);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}