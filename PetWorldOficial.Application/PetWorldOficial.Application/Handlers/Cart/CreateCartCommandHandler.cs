using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Handlers.Cart
{
    public class CreateCartCommandHandler(
        ICartService cartService,
        HttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCartCommand, CartDetailsViewModel?>
    {
        public async Task<CartDetailsViewModel?> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var context = httpContextAccessor.HttpContext;  

                if(context.Request.Cookies.TryGetValue("CartId", out var value))
                {
                }

                var cart = new Domain.Entities.Cart { };

                var cartCreated = await cartService.CreateAsync(request, cancellationToken);

                if (cartCreated is null)
                    throw new Exception();

                SetCartCookie(cartCreated.Id);

                return cartCreated;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void SetCartCookie(int cartId, HttpContext context)
        {
            var cookieOptons = new CookieOptions
            {
                Secure = false,
                Expires = DateTimeOffset.Now.AddDays(3),
                HttpOnly = true
            };

            context.Response.Cookies.Append("CartId", cartId.ToString(), cookieOptons);
        }
    }
}
