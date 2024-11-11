using PetWorldOficial.Application.Commands.CartItem;
using PetWorldOficial.Application.ViewModels.CartItem;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface ICartItemService
{
    Task DeleteCartItem(DeleteItemCommand command, CancellationToken cancellationToken);
    Task<CartItemDetailsViewModel?> GetById(int id, CancellationToken cancellationToken);
}