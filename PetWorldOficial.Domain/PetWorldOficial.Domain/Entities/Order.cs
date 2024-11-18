using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Domain.Entities;

public class Order : Entity
{
    public Order()
    {
    }

    public Order(
        int clientId,
        string? stripeSessionId,
        EPaymentMethod paymentMethod)
    {
        ClientId = clientId;
        PaymentMethod = paymentMethod;
        Code = Guid.NewGuid();
        StripeSessionId = stripeSessionId;
        Status = EOrderStatus.PaymentConfirmed;
        CreatedAt = DateTime.Now;
        PaymentDate = DateTime.Now;
        Items = new List<OrderItem>();
    }

    public int ClientId { get; private set; }
    public User Client { get; private set; }
    public Guid Code { get; private set; }
    public string? StripeSessionId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public EOrderStatus Status { get; private set; }
    public EPaymentMethod PaymentMethod { get; private set; }
    public decimal TotalPrice { get; private set; }
    public List<OrderItem> Items { get; private set; }

    public bool AddItems(params OrderItem[] items)
    {
        if (items is null || !items.Any())
            return false;

        foreach (var item in items)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem is null)
                Items.Add(item);
        }

        CalculateTotalPrice();
        return true;
    }

    public void CalculateTotalPrice()
    {
        if (!Items.Any())
        {
            TotalPrice = 0;
            return;
        }

        TotalPrice = Items.Sum(x => x.TotalPrice);
    }

    public void UpdateStatusToAwaitingPickUp()
    {
        if (Status is not EOrderStatus.PaymentConfirmed)
            throw new UnableToChangeOrderStatusException(
                "Para passar o pedido para o status \"Aguardando Coleta\" é preciso que o pedido esteja no status \"Pagamento Confirmado\"");

        Status = EOrderStatus.AwaitingPickup;
    }

    public void UpdateStatusToInTransit()
    {
        if (Status is not EOrderStatus.AwaitingPickup)
            throw new UnableToChangeOrderStatusException(
                "Para passar o pedido para o status \"Em Trânsito\" é preciso que o pedido esteja no status \"Aguardando Coleta\"");

        Status = EOrderStatus.InTransit;
    }

    public void UpdateStatusToDelivered()
    {
        if (Status is not EOrderStatus.InTransit)
            throw new UnableToChangeOrderStatusException(
                "Para passar o pedido para o status \"Entregue\" é preciso que o pedido esteja no status \"Em Trânsito\"");

        Status = EOrderStatus.Delivered;
    }

    public void UpdateStatusToCanceled()
    {
        Status = EOrderStatus.Canceled;
    }
}