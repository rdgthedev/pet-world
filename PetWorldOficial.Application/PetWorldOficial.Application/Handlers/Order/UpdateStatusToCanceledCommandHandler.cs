using AutoMapper;
using MediatR;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Order
{
    public class UpdateStatusToCanceledCommandHandler(
        IOrderService orderService,
        IEmailSenderService emailSenderService,
        IMapper mapper) : IRequestHandler<UpdateStatusToCanceledCommand, (int statusCode, string message)>
    {
        public async Task<(int statusCode, string message)> Handle(UpdateStatusToCanceledCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var orderDetails = await orderService.GetByIdAsync(request.Id, cancellationToken);

                if (orderDetails is null)
                    throw new OrderNotFoundException("Pedido não encontrado!");

                var order = mapper.Map<Domain.Entities.Order>(orderDetails);

                order.UpdateStatusToCanceled();
                await orderService.UpdateAsync(order, cancellationToken);

                await emailSenderService.SendAsync(
                    order.Client.Email!,
                    $"Pedido - {order.Id}",
                    "Seu pedido foi cancelado, nos desculpe por qualquer transtorno.\n\n" +
                    "Atencionsamente,\nEquipe Pet World");

                return (statusCode: 200, $"O pedido de número {order.Id} foi cancelado com sucesso!");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}