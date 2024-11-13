using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Commands.CartItem;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Handlers.Cart;

public class DeleteItemCommandHandler(
    ICartItemService cartItemService,
    ICartService cartService,
    ICartRepository cartRepository,
    IHttpContextAccessor httpContextAccessor,
    IUserService userService,
    IMapper mapper) : IRequestHandler<DeleteItemCommand, (bool success, decimal totalPrice)>
{
    public async Task<(bool success, decimal totalPrice)> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var email = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            
            var userId = (await userService.GetByEmailAsync(email, cancellationToken))?.Id ?? 0;

            var cartResult = await cartService.GetOrCreateCartForUserAsync(userId, cancellationToken);

            var cartItem = await cartItemService.GetById(request.Id, cancellationToken);

            if (cartItem is null)
                throw new Exception();
            
            await cartItemService.DeleteCartItem(mapper.Map<DeleteItemCommand>(cartItem), cancellationToken);

            var cart = mapper.Map<Domain.Entities.Cart>(cartResult);
            cart.RemoveItem(cartItem.ProductId);

            await cartService.UpdateAsync(mapper.Map<UpdateCartCommand>(cart), cancellationToken);
            
            return (true, cart.TotalPrice);
        }
        catch (Exception)
        {
            throw;
        }
    }
}