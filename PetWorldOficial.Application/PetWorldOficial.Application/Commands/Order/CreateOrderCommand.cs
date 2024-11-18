using MediatR;

namespace PetWorldOficial.Application.Commands.Order;

public class CreateOrderCommand : IRequest<(bool success, string message)>

{
    public int ClientId { get; set; }
    public string? SessionId { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public List<Domain.Entities.OrderItem> Items { get; set; }
}