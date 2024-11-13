using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Services;
using Stripe.Checkout;

namespace PetWorldOficial.Application.Handlers.Order;

public class CreateOrderCommandHandler(
    IHttpContextAccessor httpContextAccessor,
    IOrderService orderService,
    IUserService userService,
    ICartService cartService,
    IMapper mapper) : IRequestHandler<CreateOrderCommand, (bool success, string message)>
{
    public async Task<(bool success, string message)> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            Session stripeSession = null!;

            if (request.SessionId is not null)
            {
                request.SessionId = request.SessionId.Replace("{", "").Replace("}", "");
                stripeSession = await GetStripeSession(request.SessionId.Trim(), cancellationToken);

                if (stripeSession is null || stripeSession.PaymentStatus != "paid")
                    throw new Exception(
                        "O pagamento não foi finalizado. Não conseguimos gerar seu pedido. Tente novamente.");
            }

            // if (!Enum.TryParse<EPaymentMethod>(request.PaymentMethod, true, out var paymentMethod))
            //     throw new Exception("Método de pagamento inválido!");

            var email = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;

            var user = await userService.GetByEmailAsync(email, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Usuário não encontrado!");

            var cart = await cartService.GetCartByUserId(user.Id, cancellationToken);

            if (cart?.Items is null || !cart.Items.Any())
                throw new Exception("Carrinho vazio ou não encontrado. Verifique se há itens no seu carrinho.");

            var orderDetailsViewModel = await orderService.CreateAsync(request, cancellationToken);

            var orderEntity = mapper.Map<Domain.Entities.Order>(orderDetailsViewModel);
            orderEntity.AddItems(cart.Items!.ToArray());

            await cartService.DeleteAsync(mapper.Map<DeleteCartCommand>(cart), cancellationToken);
            return (success: true, message: "Recebemos seu pagamento! Seu pedido foi criado com sucesso.");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static async Task<Session> GetStripeSession(string sessionId, CancellationToken cancellationToken)
        => await new SessionService().GetAsync(sessionId, cancellationToken: cancellationToken);
}