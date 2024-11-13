using PetWorldOficial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.ViewModels.Order;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken);
        Task<Cart?> GetByIdAsync(int id);
        Task<OrderDetailsViewModel> CreateAsync(CreateOrderCommand command, CancellationToken cancellationToken);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(Cart cart);
    }
}