using AutoMapper;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Order;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class OrderService(
    IOrderRepository orderRepository,
    IMapper mapper) : IOrderService
{
    public async Task<IEnumerable<OrderDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var ordersEntity = await orderRepository.GetAllAsync(cancellationToken);
        var orders = mapper.Map<IEnumerable<OrderDetailsViewModel>>(ordersEntity);
        return orders;
    }

    public async Task<IEnumerable<OrderDetailsViewModel>> GetAllByClientId(
        int clientId,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<OrderDetailsViewModel>>(
            await orderRepository.GetAllByClientId(clientId, cancellationToken));
    }


    public async Task<OrderDetailsViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return mapper.Map<OrderDetailsViewModel>(await orderRepository.GetByIdAsync(id, cancellationToken));
    }

    public async Task<OrderDetailsViewModel> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        return mapper.Map<OrderDetailsViewModel>(await orderRepository.AddAsync(
            order,
            cancellationToken));
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        await orderRepository.UpdateAsync(order, cancellationToken);
    }

    public Task DeleteAsync(Cart cart)
    {
        throw new NotImplementedException();
    }
}