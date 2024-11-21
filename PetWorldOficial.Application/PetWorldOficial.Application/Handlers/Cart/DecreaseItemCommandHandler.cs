using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Cart;

public class DecreaseItemCommandHandler(
    ICartService cartService,
    IProductService productService,
    IUserService userService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<DecreaseItemCommand, (bool success, decimal subTotalPrice, decimal totalPrice)>
{
    public async Task<(bool success, decimal subTotalPrice, decimal totalPrice)> Handle(DecreaseItemCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await productService.GetById(request.ProductId, cancellationToken);

            if (product is null)
                throw new ProductNotFoundException("Produto não encontrado!");

            var email = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;

            var userId = (await userService.GetByEmailAsync(email, cancellationToken))?.Id ?? 0;

            var cart = await cartService.GetOrCreateCartForUserAsync(userId, cancellationToken);
            var cartEntity = mapper.Map<Domain.Entities.Cart>(cart);

            foreach (var item in cartEntity.Items.Where(item => item.ProductId == product.Id))
                cartEntity.DecreaseItem(item);

            await cartService.UpdateAsync(mapper.Map<UpdateCartCommand>(cartEntity), cancellationToken);
            
            return (true, cartEntity.SubTotalPrice, cartEntity.TotalPrice);
        }
        catch (Exception)
        {
            throw;
        }
    }
}