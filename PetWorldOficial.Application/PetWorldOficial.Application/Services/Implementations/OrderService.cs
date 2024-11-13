using AutoMapper;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Order;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class OrderService(
    IOrderRepository orderRepository,
    IMapper mapper) : IOrderService
{
    public Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Cart?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderDetailsViewModel> CreateAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        return mapper.Map<OrderDetailsViewModel>(await orderRepository.AddAsync(mapper.Map<Order>(command), cancellationToken));
    }

    public Task UpdateAsync(Cart cart)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Cart cart)
    {
        throw new NotImplementedException();
    }
}