using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories
{
    public class CartRepository(
        AppDbContext context) : ICartRepository
    {
        public async Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Carts.ToListAsync(cancellationToken);
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await context.Carts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Cart cart)
        {
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cart cart)
        {
            context.Remove(cart);
            await context.SaveChangesAsync();
        }
    }
}
