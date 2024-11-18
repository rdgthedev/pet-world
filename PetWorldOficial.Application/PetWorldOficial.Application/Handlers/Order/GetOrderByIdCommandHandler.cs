using MediatR;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Order;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Order;

public class GetOrderByIdCommandHandler(
    IOrderService orderService) : IRequestHandler<GetOrderByIdCommand, OrderDetailsViewModel>
{
    public async Task<OrderDetailsViewModel> Handle(GetOrderByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderService.GetByIdAsync(request.Id, cancellationToken);

            if (order is null)
                throw new OrderNotFoundException("Pedido não encontrado!");

            return order;
        }
        catch (Exception)
        {
            throw;
        }
    }
}