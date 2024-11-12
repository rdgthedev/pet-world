using AutoMapper;
using PetWorldOficial.Application.Services.Interfaces;
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

    public async Task CreateAsync(Order order, CancellationToken cancellationToken)
    {
        await orderRepository.AddAsync(mapper.Map<Order>(order), cancellationToken);
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