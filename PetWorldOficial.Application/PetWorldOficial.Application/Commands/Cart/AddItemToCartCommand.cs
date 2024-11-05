using MediatR;

namespace PetWorldOficial.Application.Commands.Cart;

public class AddItemToCartCommand : IRequest<Unit>
{
    public int ProductId { get; set; }
}