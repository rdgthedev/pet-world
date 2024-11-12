using MediatR;

namespace PetWorldOficial.Application.Commands.Order;

public class CreateOrderCommand : IRequest<(bool success, string message)>
{
}