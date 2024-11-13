using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface ICartItemRepository
{
    Task DeleteRangeAsync(List<CartItem> items, CancellationToken cancellationToken);
    Task DeleteAsync(CartItem item, CancellationToken cancellationToken);
    Task<CartItem?> GetById(int id, CancellationToken cancellationToken);
}