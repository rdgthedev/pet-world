using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Stock;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using Stripe.Checkout;

namespace PetWorldOficial.Application.Handlers.Order;

public class CreateOrderCommandHandler(
    IHttpContextAccessor httpContextAccessor,
    IOrderService orderService,
    IUserService userService,
    ICartService cartService,
    IEmailSenderService emailSenderService,
    IProductService productService,
    IStockService stockService,
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
                throw new CartNotFoundException(
                    "Carrinho vazio ou não encontrado. Adicione itens ao seu carrinho antes de fazer um pedido!.");

            var invalidItems = await VerifyCartItemsInStock(cart.Items, cancellationToken);

            if (invalidItems.Any())
            {
                cart.Items = cart.Items.Where(c => !invalidItems.Contains(c)).ToList();
                await cartService.UpdateAsync(mapper.Map<UpdateCartCommand>(cart), cancellationToken);

                var invalidProductNames = invalidItems.Select(item => item.Product.Name).ToList();

                throw new QuantityOfProductOutOfStockException(
                    $"Os seguintes produtos foram retirados do " +
                    $"seu carrinho, pois não estão mais em estoque: {string.Join(", ", invalidProductNames)}");
            }

            await cartService.DeleteAsync(mapper.Map<DeleteCartCommand>(cart), cancellationToken);

            var order = new PetWorldOficial.Domain.Entities.Order(
                user.Id,
                stripeSession?.Id,
                paymentMethod);

            var items = cart.Items
                .Select(i => new OrderItem(i.ProductId, order.Id, i.Quantity, i.Product.Price))
                .ToArray();

            order.AddItems(items);

            await orderService.CreateAsync(order, cancellationToken);

            foreach (var item in items)
            {
                var stockDetailsViewModel = await stockService.GetByProductIdAsync(item.ProductId, cancellationToken);
                await DecreaseItemsInStock(stockDetailsViewModel, item.Quantity, cancellationToken);
            }

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

    private async Task<List<CartItem>> VerifyCartItemsInStock(List<CartItem> items, CancellationToken cancellationToken)
    {
        var notInStock = new List<CartItem>();

        foreach (var item in items)
            if (!await stockService.ValidateStockQuantity(item.ProductId, item.Quantity, cancellationToken))
                notInStock.Add(item);

        if (!notInStock.Any())
            return [];

        return notInStock;
    }

    private async Task DecreaseItemsInStock(
        StockDetailsViewModel stockDetailsViewModel,
        int quantity,
        CancellationToken cancellationToken)
    {
        stockDetailsViewModel.Quantity -= quantity;
        await stockService.UdpateAsync(stockDetailsViewModel, cancellationToken);
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