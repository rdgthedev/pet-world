using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Order : Entity
{
    public Order()
    {
    }

    public Order(
        int clientId,
        CartItem item)
    {
        ClientId = clientId;
        Status = EOrderStatus.WaitingPayment;
        Items = new List<CartItem>();
        Items.Add(item);
        TotalPrice = TotalValue();
    }

    public int ClientId { get; private set; }
    public User Client { get; private set; }
    public Guid Code { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public EOrderStatus Status { get; private set; }
    public EPaymentMethod PaymentMethod { get; private set; }
    public decimal TotalPrice { get; private set; }
    public List<CartItem> Items { get; private set; }

    public decimal TotalValue()
    {
        decimal totalValue = 0;

        if (!Items.Any())
            return totalValue;

        Items.ForEach(i => totalValue += i.TotalPrice);

        return totalValue;
    }
}