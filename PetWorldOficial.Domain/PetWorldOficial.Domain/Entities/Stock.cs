using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Stock : Entity
{
    protected Stock()
    {
    }

    public Stock(
        int productId,
        int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
        CreatedAt = DateTime.Now;
    }

    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }

    public void DecreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            return;

        Quantity = quantity;
    }
}