using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.ViewModels.Cart;
using PetWorldOficial.Application.ViewModels.CartItem;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Handlers.Cart
{
    public class CreateCartCommandHandler(
        ICartRepository cartRepository,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCartCommand, CartDetailsViewModel>
    {
        private const string _cartKeyCookie = "CartId";

        public async Task<CartDetailsViewModel> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var context = httpContextAccessor.HttpContext;

                if (!ExistsCartCookie(context))
                {
                    var cart = await CreateCart(context, cancellationToken);
                    SetCartCookie(cart.Id, 90, context);

                    return GetResponse(cart);
                }

                var cartId = GetCartCookieValue(context);

                var cartResult = await cartRepository.GetByIdAsync(cartId!.Value, cancellationToken);

                if (cartResult is null || cartResult.ExpiresDate < DateTime.Now)
                {
                    if (cartResult is not null)
                    {
                        await cartRepository.DeleteAsync(cartResult, cancellationToken);
                        SetCartCookie(cartResult.Id, -1, context);
                    }

                    cartResult = await CreateCart(context, cancellationToken);
                    SetCartCookie(cartResult.Id, 90, context);

                    return GetResponse(cartResult);
                }

                return GetResponse(cartResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetCartCookie(int cartId, int expiresDays, HttpContext context)
        {
            var cookieOptons = new CookieOptions
            {
                Secure = false,
                Expires = DateTimeOffset.Now.AddDays(expiresDays),
                HttpOnly = true
            };

            httpContextAccessor.HttpContext.Response.Cookies.Append(_cartKeyCookie, cartId.ToString(), cookieOptons);
        }

        private async Task<Domain.Entities.Cart> CreateCart(
            HttpContext context,
            CancellationToken cancellationToken)
        {
            var cart = await cartRepository.AddAsync(new Domain.Entities.Cart(null!), cancellationToken);

            if (cart is null)
                throw new Exception("Ocorreu um erro ao criar o carrinho!");

            return cart;
        }

        private bool ExistsCartCookie(HttpContext context)
            => context.Request.Cookies.ContainsKey(_cartKeyCookie);

        private int? GetCartCookieValue(HttpContext context)
            => !context.Request.Cookies.TryGetValue(_cartKeyCookie, out var value)
                ? default!
                : Convert.ToInt32(value);

        private CartDetailsViewModel GetResponse(Domain.Entities.Cart cart)
        {
            var cartItems = cart
                .Items
                .Select(c => new CartItemDetailsViewModel
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    Description = c.Product.Description,
                    ImageUrl = c.Product.ImageUrl,
                    CartId = c.CartId,
                    Quantity = c.Quantity,
                    Price = c.Product.Price,
                    TotalPrice = c.TotalPrice,
                    OrderId = c.OrderId
                })
                .ToList();

            var response = new CartDetailsViewModel
            {
                Id = cart.Id,
                ExpiresDate = cart.ExpiresDate,
                Items = cartItems,
                TotalPrice = cart.TotalPrice
            };

            return response;
        }
    }
}