using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface ICartCookieService
{
    bool ExistsCartCookie(HttpContext context);

    Task<CartDetailsViewModel?> GetCartFromCookieAsync(
        HttpContext context,
        CancellationToken cancellationToken);

    void SetCartCookie(string value, DateTime expiresDate, HttpContext context);
    int? GetCartCookieValue(HttpContext context);
}