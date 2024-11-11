using MediatR;

namespace PetWorldOficial.Application.Commands.Cart;

public class AddOrIncreaseItemToCartCommand : IRequest<(bool success, decimal totalPrice)>
{
    public int ProductId { get; set; }
}