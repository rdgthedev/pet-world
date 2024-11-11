using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Cart;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Cart;

public class GetOrCreateCartCommandHandler(
    ICartService cartService,
    IUserService userService,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetOrCreateCartCommand, CartDetailsViewModel>
{
    public async Task<CartDetailsViewModel> Handle(GetOrCreateCartCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var context = httpContextAccessor.HttpContext;
            var user = context.User;

            var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            var userId = (await userService.GetByEmailAsync(email, cancellationToken))?.Id ?? default;

            if (userId is 0)
                throw new UserNotFoundException("Faça o login ou cadastre-se!");

            var cart = await cartService.GetOrCreateCartForUserAsync(userId, cancellationToken);

            return cart;
        }
        catch (Exception)
        {
            throw;
        }
    }
}