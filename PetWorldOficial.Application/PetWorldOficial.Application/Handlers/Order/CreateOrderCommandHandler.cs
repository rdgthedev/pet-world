using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
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
    IEmailSenderService emailSenderService,
    IMapper mapper) : IRequestHandler<CreateOrderCommand, (bool success, string message)>
{
    public async Task<(bool success, string message)> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var paymentMethod = ParsePaymentMethod(request.PaymentMethod);

            Session stripeSession = null!;

            if (request.SessionId is not null)
            {
                request.SessionId = request.SessionId.Replace("{", "").Replace("}", "");
                stripeSession = await GetStripeSession(request.SessionId.Trim(), cancellationToken);

                if (stripeSession is null || stripeSession.PaymentStatus != "paid")
                    throw new Exception(
                        "O pagamento não foi finalizado. Não conseguimos gerar seu pedido. Tente novamente.");
            }

            var email = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;

            var user = await userService.GetByEmailAsync(email, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Usuário não encontrado!");

            var cart = await cartService.GetCartByUserId(user.Id, cancellationToken);

            if (cart?.Items is null || !cart.Items.Any())
                throw new Exception("Carrinho vazio ou não encontrado. Verifique se há itens no seu carrinho.");

            await cartService.DeleteAsync(mapper.Map<DeleteCartCommand>(cart), cancellationToken);

            var order = new PetWorldOficial.Domain.Entities.Order(user.Id, stripeSession?.Id, paymentMethod);

            var items = cart.Items
                .Select(i => new OrderItem(i.ProductId, order.Id, i.Quantity, i.Product.Price))
                .ToArray();

            order.AddItems(items);

            await orderService.CreateAsync(order, cancellationToken);
            
            await emailSenderService.SendAsync(
                user.Email, 
                $"Pedido - {order.Id}",
                "Seu pagamento foi confirmado, em breve lhe enviaremos atualizações sobre o seu pedido.\n\n" +
                "Atencionsamente,\nEquipe Pet World");
            
            return (success: true, message: "Recebemos seu pagamento! Seu pedido foi criado com sucesso.");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static EPaymentMethod ParsePaymentMethod(string paymentMethod)
    {
        if (Enum.TryParse<EPaymentMethod>(paymentMethod, true, out var result))
            return result;

        throw new Exception("Método de pagamento inválido.");
    }

    private static async Task<Session> GetStripeSession(string sessionId, CancellationToken cancellationToken)
        => await new SessionService().GetAsync(sessionId, cancellationToken: cancellationToken);
}