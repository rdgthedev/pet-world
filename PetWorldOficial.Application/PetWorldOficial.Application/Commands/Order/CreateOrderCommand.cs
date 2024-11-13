using MediatR;

namespace PetWorldOficial.Application.Commands.Order;

public class CreateOrderCommand : IRequest<(bool success, string message)>

{
    public string? SessionId { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}