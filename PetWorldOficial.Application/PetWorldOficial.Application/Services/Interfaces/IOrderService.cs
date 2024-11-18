using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Application.ViewModels.Order;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<OrderDetailsViewModel>> GetAllByClientId(int clientId, CancellationToken cancellationToken);
        Task<OrderDetailsViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<OrderDetailsViewModel> CreateAsync(Order command, CancellationToken cancellationToken);
        Task UpdateAsync(Order order, CancellationToken cancellationToken);
        Task DeleteAsync(Cart cart);
    }
}