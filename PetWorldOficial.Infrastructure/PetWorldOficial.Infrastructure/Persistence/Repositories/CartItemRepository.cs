using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Data.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class CartItemRepository(AppDbContext context) : ICartItemRepository
{
    public async Task DeleteRangeAsync(List<CartItem> items, CancellationToken cancellationToken)
    {
        // Desanexa qualquer entidade relacionada rastreada que possa causar conflitos
        foreach (var entry in context.ChangeTracker.Entries())
        {
            entry.State = EntityState.Detached;
        }

        // Garante que apenas as entidades passadas no parâmetro sejam manipuladas
        foreach (var item in items)
        {
            // Anexa o item ao contexto se não estiver rastreado
            if (context.Entry(item).State == EntityState.Detached)
            {
                context.CartItems.Attach(item);
            }
        }

        // Remove todos os itens de uma vez
        context.CartItems.RemoveRange(items);

        // Salva as alterações
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