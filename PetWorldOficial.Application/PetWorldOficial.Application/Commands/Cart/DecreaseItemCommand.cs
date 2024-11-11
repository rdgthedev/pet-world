using MediatR;

namespace PetWorldOficial.Application.Commands.Cart;

public class DecreaseItemCommand : IRequest<(bool success, decimal totalPrice)>
{
    public int ProductId { get; set; }
}