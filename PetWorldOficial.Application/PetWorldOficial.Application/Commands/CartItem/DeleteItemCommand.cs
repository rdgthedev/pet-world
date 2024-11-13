using MediatR;

namespace PetWorldOficial.Application.Commands.CartItem;

public class DeleteItemCommand : IRequest<(bool success, decimal totalPrice)>
{
    public int Id { get; set; }
}