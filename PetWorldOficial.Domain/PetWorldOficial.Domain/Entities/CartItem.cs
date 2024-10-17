using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class CartItem : Entity
{
    public CartItem()
    {
    }

    public CartItem(
        Product product,
        int cartId,
        int quantity)
    {
        Product = product;
        CartId = cartId;
        Quantity = quantity;
    }

    public CartItem(
        Product product,
        int quantity)
    {
        Product = product;
        Quantity = quantity;
        TotalPrice = TotalValue();
    }

    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public int CartId { get; private set; }
    public Cart Cart { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal TotalPrice { get; private set; }
    public int OrderId { get; private set; }
    public Order Order { get; private set; }

    private decimal TotalValue()
        => Price * Quantity;
}