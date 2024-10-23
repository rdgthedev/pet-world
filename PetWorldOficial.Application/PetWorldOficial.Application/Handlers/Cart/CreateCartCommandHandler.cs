using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Handlers.Cart
{
    public class CreateCartCommandHandler(
        ICartService cartService,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCartCommand, CartDetailsViewModel?>
    {
        private const string _cartKeyCookie = "CartId";

        public async Task<CartDetailsViewModel?> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(_cartKeyCookie, out var value))
                {
                    var cartId = Convert.ToInt32(value);
                    var cartResult = await cartService.GetByIdAsync(cartId, cancellationToken);

                    if (cartResult is not null && cartResult.ExpiresDate >= DateTime.Now)
                        return cartResult;

                    //Setar as propriedades do DeleteCartCommand
                    await cartService.DeleteAsync(new DeleteCartCommand { }, cancellationToken);
                    SetCartCookie(Convert.ToInt32(value), -1);
                }

                var cartCreated = await cartService.CreateAsync(request, cancellationToken);

                if (cartCreated is null)
                    throw new Exception("Ocorreu um erro interno!");

                SetCartCookie(cartCreated.Id, 3);

                return cartCreated;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetCartCookie(int cartId, int expiresDays)
        {
            var cookieOptons = new CookieOptions
            {
                Secure = false,
                Expires = DateTimeOffset.Now.AddDays(expiresDays),
                HttpOnly = true
            };

            httpContextAccessor.HttpContext.Response.Cookies.Append(_cartKeyCookie, cartId.ToString(), cookieOptons);
        }
    }
}