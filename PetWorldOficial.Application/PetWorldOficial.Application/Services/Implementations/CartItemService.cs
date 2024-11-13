using AutoMapper;
using PetWorldOficial.Application.Commands.CartItem;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.CartItem;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class CartItemService(
    ICartItemRepository cartItemRepository,
    IMapper mapper) : ICartItemService
{
    public async Task DeleteCartItem(DeleteItemCommand command, CancellationToken cancellationToken)
    {
        await cartItemRepository.DeleteAsync(mapper.Map<CartItem>(command), cancellationToken);
    }

    public async Task<CartItemDetailsViewModel?> GetById(int id, CancellationToken cancellationToken)
    {
        return mapper.Map<CartItemDetailsViewModel>(await cartItemRepository.GetById(id, cancellationToken));
    }
}