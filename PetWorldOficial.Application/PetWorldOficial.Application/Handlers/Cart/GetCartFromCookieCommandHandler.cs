using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Cart;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Cart;

public class GetCartFromCookieCommandHandler(
    ICartService cartService,
    ICartCookieService cartCookieService,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetCartFromCookieCommand, CartDetailsViewModel>
{
    public async Task<CartDetailsViewModel> Handle(GetCartFromCookieCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await cartCookieService.GetCartFromCookieAsync(httpContextAccessor.HttpContext, cancellationToken);

            if (cart is null)
                throw new CartNotFoundException("Carrinho não encontrado!");

            return cart;
        }
        catch (Exception)
        {
            throw;
        }
    }
}