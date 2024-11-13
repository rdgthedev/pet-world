namespace PetWorldOficial.Application.ViewModels.Order;

public class OrderDetailsViewModel
{
    public int ClientId { get; set; }
    public Domain.Entities.User Client { get; set; } = null!;
    public Guid Code { get; set; }
    public string? StripeSession { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public List<Domain.Entities.CartItem> Items { get; set; } = null!;
}