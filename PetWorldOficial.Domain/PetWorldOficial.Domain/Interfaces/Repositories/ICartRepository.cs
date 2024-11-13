using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken);
        Task<Cart> AddAsync(Cart cart, CancellationToken cancellationToken);
        Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Cart cart, CancellationToken cancellationToken);
        Task DeleteAsync(Cart cart, CancellationToken cancellationToken);
        Task<Cart?> GetCartByUserId(int id, CancellationToken cancellationToken);
    }
}