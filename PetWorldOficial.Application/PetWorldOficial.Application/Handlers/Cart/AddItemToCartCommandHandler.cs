using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Cart;
using PetWorldOficial.Application.ViewModels.Product;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Cart;

public class AddItemToCartCommandHandler(
    ICartService cartService,
    IProductService productService,
    IUserService userService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AddOrIncreaseItemToCartCommand, (bool success, decimal subTotalPrice, decimal totalPrice)>
{
    public async Task<(bool success, decimal subTotalPrice, decimal totalPrice)> Handle(
        AddOrIncreaseItemToCartCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await productService.GetById(request.ProductId, cancellationToken);

            if (product is null)
                throw new ProductNotFoundException("Produto não encontrado!");

            var email = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;

            var userId = (await userService.GetByEmailAsync(email, cancellationToken))?.Id ?? 0;

            var cart = await cartService.GetOrCreateCartForUserAsync(userId, cancellationToken);

            var item = cart.Items?.FirstOrDefault(i => i.ProductId == product.Id);

            if (item?.Quantity >= product.Stock.Quantity)
                throw new QuantityOfProductOutOfStockException(
                    "Você atingiu a quantidade máxima desse item no carrinho!");
            
            var cartEntity = mapper.Map<Domain.Entities.Cart>(cart);
            var success = cartEntity.AddItem(new CartItem(product.Id, cart.Id, 1, product.Price),
                product.Stock.Quantity);
            await cartService.UpdateAsync(mapper.Map<UpdateCartCommand>(cartEntity), cancellationToken);
            
            return (success, cartEntity.SubTotalPrice, cartEntity.TotalPrice); 
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static CartDetailsViewModel AddOrIncreaseItems(
        CartDetailsViewModel cart,
        ProductDetailsViewModel product)
    {
        var existingItem = cart.Items?.FirstOrDefault(c => c.ProductId == product.Id);

        if (existingItem == null)
        {
            cart.Items ??= [];
            cart.Items.Add(new CartItem(product.Id, cart.Id, 1, product.Price));
        }
        else
            existingItem.IncreaseQuantity(1);

        return cart;
    }
}