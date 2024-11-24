using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Cart;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class CartService(
    ICartRepository cartRepository,
    ICartCookieService cartCookieService,
    IHttpContextAccessor httpContextAccessor,
    IStockService stockService,
    ICartItemRepository cartItemRepository,
    IMapper mapper) : ICartService
{
    private const int _freightPrice = 10;

    public async Task<CartDetailsViewModel> CreateAsync(
        int? userId,
        CancellationToken cancellationToken)
    {
        var cart = await cartRepository.AddAsync(new Cart(userId!.Value), cancellationToken);

        if (cart is null)
            throw new Exception();

        return new CartDetailsViewModel
        {
            Id = cart.Id,
            ClientId = cart.ClientId,
            ExpiresDate = cart.ExpiresDate,
            Items = cart.Items.Select(x => new CartItem(x.Product, cart.Id, 1)).ToList(),
            TotalPrice = cart.Items.Sum(i => i.TotalPrice)
        };
    }

    public async Task DeleteAsync(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(command.Id!.Value, cancellationToken);

        if (cart is null)
            throw new Exception();

        await cartRepository.DeleteAsync(cart, cancellationToken);
    }

    public async Task<CartDetailsViewModel?> GetCartByUserId(int userId, CancellationToken cancellationToken)
    {
        return mapper.Map<CartDetailsViewModel?>(await cartRepository.GetCartByUserId(userId, cancellationToken));
    }

    public async Task<CartDetailsViewModel> GetOrCreateCartForUserAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        var cartResult = await cartRepository.GetCartByUserId(userId, cancellationToken);
        var cart = cartResult != null ? mapper.Map<CartDetailsViewModel>(cartResult) : null!;

        if (cart?.ExpiresDate < DateTime.Now)
        {
            await cartRepository.DeleteAsync(mapper.Map<Cart>(cart), cancellationToken);
            cartCookieService.SetCartCookie(string.Empty, DateTime.Now.AddDays(-1), httpContextAccessor.HttpContext);
            cart = null;
        }

        if (cart is not null)
        {
            if (httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated && !CartOwnerExists(cart))
            {
                cart.ClientId = userId;
                await UpdateAsync(mapper.Map<UpdateCartCommand>(cart), cancellationToken);
            }

            var cookieCartId = cartCookieService.GetCartCookieValue(httpContextAccessor.HttpContext);

            if (cookieCartId is null || cookieCartId != cart.Id)
                cartCookieService.SetCartCookie(cart.Id.ToString(), cart.ExpiresDate, httpContextAccessor.HttpContext);

            var itemsToRemove = new List<CartItem>();

            foreach (var item in cart.Items!)
            {
                var isValid =
                    await stockService.ValidateStockQuantity(item.ProductId, item.Quantity, cancellationToken);

                if (!isValid)
                    itemsToRemove.Add(item);
            }

            if (itemsToRemove.Any())
            {
                await cartItemRepository.DeleteRangeAsync(itemsToRemove, cancellationToken);
                cart.Items = cart.Items.Where(ci => !itemsToRemove.Contains(ci)).ToList();

                if (cart.Items.Count <= 0)
                    throw new QuantityOfProductOutOfStockException(
                        "Infelizmente os itens que estavam em seu carrinho esgotaram no estoque!");
            }

            return cart;
        }

        var cartCreated = await CreateAsync(userId, cancellationToken);

        cartCookieService.SetCartCookie(
            cartCreated.Id.ToString(),
            cartCreated.ExpiresDate,
            httpContextAccessor.HttpContext);

        cartCreated.TotalPrice += _freightPrice;

        return cartCreated;
    }

    public async Task<IEnumerable<CartDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<CartDetailsViewModel>>(await cartRepository.GetAllAsync(cancellationToken));
    }

    public async Task<CartDetailsViewModel?> GetByIdAsync(int? id, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(id!.Value, cancellationToken);

        return new CartDetailsViewModel
        {
            Id = cart!.Id,
            ExpiresDate = cart.ExpiresDate,
            Items = cart.Items.Select(x => new CartItem(x.Product, cart.Id, 1)).ToList(),
            TotalPrice = cart.Items.Sum(i => i.TotalPrice)
        };
    }

    public async Task UpdateAsync(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(command.Id, cancellationToken);

        if (cart is null)
            throw new Exception();

        await cartRepository.UpdateAsync(mapper.Map(command, cart), cancellationToken);
    }

    public bool CartOwnerExists(CartDetailsViewModel? cart)
    {
        return (bool)cart?.ClientId.HasValue;
    }
}