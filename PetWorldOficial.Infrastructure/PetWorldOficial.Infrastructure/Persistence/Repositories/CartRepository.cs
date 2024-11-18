using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Data.Context;

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
            return await context
                .Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Stock)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken)
        {
            var trackedUser = context.ChangeTracker.Entries<User>()
                .FirstOrDefault(e => e.Entity.Id == cart.ClientId);

            if (trackedUser != null)
            {
                trackedUser.State = EntityState.Detached;
            }

            foreach (var item in cart.Items)
            {
                var trackedProduct = context.ChangeTracker.Entries<Product>()
                    .FirstOrDefault(e => e.Entity.Id == item.ProductId);

                if (trackedProduct != null)
                {
                    trackedProduct.State = EntityState.Detached;
                }
            }


            context.Carts.Update(cart);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken)
        {
            var trackedCart = context.ChangeTracker.Entries<Cart>()
                .FirstOrDefault(e => e.Entity.Id == cart.Id);

            if (trackedCart != null)
            {
                trackedCart.State = EntityState.Detached;
            }

            var trackedUser = context.ChangeTracker.Entries<User>()
                .FirstOrDefault(e => e.Entity.Id == cart.ClientId);

            if (trackedUser != null)
            {
                trackedUser.State = EntityState.Detached;
            }

            context.Carts.Remove(cart);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Cart?> GetCartByUserId(int id, CancellationToken cancellationToken)
        {
            var cart = await context
                .Carts
                .AsNoTracking()
                .Include(c => c.Client)
                .Include(c => c.Items)
                .ThenInclude(c => c.Product)
                .ThenInclude(p => p.Stock)
                .FirstOrDefaultAsync(c => c.ClientId == id, cancellationToken);

            return cart;
        }
    }
}