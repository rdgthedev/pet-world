using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class OrderItem : Entity
{
    public OrderItem()
    {
    }

    public OrderItem(
        Product product,
        int orderId,
        int quantity)
    {
        Product = product;
        OrderId = orderId;
        Quantity = quantity;
        TotalPrice = TotalValue();
    }

    public OrderItem(
        int productId,
        int orderId,
        int quantity,
        decimal price)
    {
        ProductId = productId;
        OrderId = orderId;
        Quantity = quantity;
        Price = price;
        TotalPrice = TotalValue();
    }

    public OrderItem(
        Product product,
        int quantity)
    {
        Product = product;
        Quantity = quantity;
        TotalPrice = TotalValue();
    }

    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal TotalPrice { get; private set; }
    public int OrderId { get; private set; }
    public Order Order { get; private set; }

    private decimal TotalValue()
        => Price * Quantity;
}