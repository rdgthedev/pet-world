namespace PetWorldOficial.Application.ViewModels.Order;

public class OrderDetailsViewModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Domain.Entities.User? Client { get; set; } = null!;
    public Guid Code { get; set; }
    public string? StripeSessionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<Domain.Entities.OrderItem> Items { get; set; } = null!;
}