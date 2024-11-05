// using MediatR;
// using Microsoft.AspNetCore.Http;
// using PetWorldOficial.Application.Commands.Cart;
// using PetWorldOficial.Application.Services.Interfaces;
//
// namespace PetWorldOficial.Application.Handlers.Cart;
//
// public class AddItemToCartCommandHandler(
//     ICartCookieService cartCookieService,
//     ICartService cartService,
//     IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddItemToCartCommand, Unit>
// {
//     public async Task<Unit> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
//     {
//         try
//         {
//             if (cartCookieService.ExistsCartCookie(httpContextAccessor.HttpContext))
//             {
//                 var cartId = cartCookieService.GetCartCookieValue(httpContextAccessor.HttpContext) ?? 0;
//
//                 var cart = await cartService.GetOrCreateCartForUserAsync();
//                 
//                 
//             }
//         }
//         catch (Exception)
//         {
//             throw;
//         }
//     }
// }