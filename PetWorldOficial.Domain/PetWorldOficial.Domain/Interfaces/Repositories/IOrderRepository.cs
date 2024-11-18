using PetWorldOficial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetAllByClientId(int clientId, CancellationToken cancellationToken);
        Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Order> AddAsync(Order order, CancellationToken cancellationToken);
        Task UpdateAsync(Order order, CancellationToken cancellationToken);
        Task DeleteAsync(Order order);
    }
}
