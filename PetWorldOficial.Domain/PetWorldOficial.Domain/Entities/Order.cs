using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Order : Entity
{
    public Order()
    {
    }

    public Order(int clientId)
    {
        ClientId = clientId;
        Status = EOrderStatus.WaitingPayment;
        Items = new List<CartItem>();
        CalculateTotalPrice();
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

    public void AddItems(params CartItem[] cartItems)
    {
        if (cartItems.Length <= 0)
            return;

        Items.AddRange(cartItems.ToList());
        CalculateTotalPrice();
    }

    private void CalculateTotalPrice()
    {
        if (!Items.Any())
            return;

        TotalPrice = Items.Sum(x => x.TotalPrice);
    }
}