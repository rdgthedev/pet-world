using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Cart;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class CartCookieService(
    ICartRepository cartRepository,
    IMapper mapper) : ICartCookieService
{
    private const string _cartKeyCookie = "CartId";

    public bool ExistsCartCookie(HttpContext context)
        => context.Request.Cookies.ContainsKey(_cartKeyCookie);

    public async Task<CartDetailsViewModel?> GetCartFromCookieAsync(HttpContext context,
        CancellationToken cancellationToken)
    {
        if (ExistsCartCookie(context))
        {
            var cartId = GetCartCookieValue(context);

            if (cartId.HasValue)
                return mapper.Map<CartDetailsViewModel>(await cartRepository.GetByIdAsync(cartId.Value, cancellationToken));
        }

        return null;
    }

    public void SetCartCookie(string value, DateTime expiresDate, HttpContext context)
    {
        var cookieOptons = new CookieOptions
        {
            Secure = false,
            Expires = expiresDate,
            HttpOnly = true
        };

        context.Response.Cookies.Append(_cartKeyCookie, value, cookieOptons);
    }

    public int? GetCartCookieValue(HttpContext context)
        => !context.Request.Cookies.TryGetValue(_cartKeyCookie, out var value)
            ? null!
            : Convert.ToInt32(value);
}