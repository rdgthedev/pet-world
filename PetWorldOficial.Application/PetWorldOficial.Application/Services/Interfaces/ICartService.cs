using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<CartDetailsViewModel?> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateCartCommand command, CancellationToken cancellationToken);
        Task<CartDetailsViewModel> CreateAsync(int? userId, CancellationToken cancellationToken);
        Task DeleteAsync(DeleteCartCommand command, CancellationToken cancellationToken);
        Task<CartDetailsViewModel?> GetCartByUserId(int userId, CancellationToken cancellationToken);

        Task<CartDetailsViewModel> GetOrCreateCartForUserAsync(
            int userId,
            CancellationToken cancellationToken);

        bool CartOwnerExists(CartDetailsViewModel? cart);
    }
}