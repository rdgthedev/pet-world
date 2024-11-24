using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Commands.Checkout;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Services;

namespace PetWorldOficial.Application.Handlers.Checkout;

public class CreateCheckoutSessionCommandHandler(
    IPaymentService paymentService,
    IUserService userService,
    ICartService cartService,
    IStockService stockService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCheckoutSessionCommand, StatusCodeResult>
{
    public async Task<StatusCodeResult> Handle(CreateCheckoutSessionCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var email = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            var userId = (await userService.GetByEmailAsync(email, cancellationToken))?.Id ?? 0;
            var cart = await cartService.GetOrCreateCartForUserAsync(userId, cancellationToken);

            var invalidItems = await VerifyCartItemsInStock(cart.Items, cancellationToken);

            if (invalidItems.Any())
            {
                cart.Items = cart.Items?.Where(c => !invalidItems.Contains(c)).ToList();
                await cartService.UpdateAsync(mapper.Map<UpdateCartCommand>(cart), cancellationToken);

                var invalidProductNames = invalidItems.Select(item => item.Product.Name).ToList();

                throw new QuantityOfProductOutOfStockException(
                    $"Os seguintes produtos foram retirados do " +
                    $"seu carrinho, pois não estão mais em estoque: {string.Join(", ", invalidProductNames)}");
            }

            var result = await paymentService.CreateSessionCheckout(email, cart.Items, cancellationToken);

            return new StatusCodeResult(result.statusCode);
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
}