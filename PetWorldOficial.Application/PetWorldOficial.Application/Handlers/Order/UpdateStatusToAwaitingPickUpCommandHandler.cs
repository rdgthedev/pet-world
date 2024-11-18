using AutoMapper;
using MediatR;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Order;

public class UpdateStatusToAwaitingPickUpCommandHandler(
    IOrderService orderService,
    IEmailSenderService emailSenderService,
    IMapper mapper) : IRequestHandler<UpdateStatusToAwaitingPickUpCommand, (int statusCode, string message)>
{
    public async Task<(int statusCode, string message)> Handle(UpdateStatusToAwaitingPickUpCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderDetails = await orderService.GetByIdAsync(request.Id, cancellationToken);

            if (orderDetails is null)
                throw new OrderNotFoundException("Pedido não encontrado!");

            var order = mapper.Map<Domain.Entities.Order>(orderDetails);

            order.UpdateStatusToAwaitingPickUp();
            await orderService.UpdateAsync(order, cancellationToken);

            await emailSenderService.SendAsync(
                order.Client.Email!,
                $"Pedido - {order.Id}",
                "Seu pedido está aguardando a coleta pela transportadora," +
                " em breve lhe enviaremos atualizações sobre o seu pedido.\n\n" +
                "Atencionsamente,\nEquipe Pet World");

            return (statusCode: 200, $"O pedido de número {order.Id} está agurdando coleta!");
        }
        catch (Exception)
        {
            throw;
        }
    }
}