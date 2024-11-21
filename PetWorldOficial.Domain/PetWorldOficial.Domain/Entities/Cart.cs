using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Cart : Entity
{
    public Cart(int? clientId)
    {
        if (clientId.HasValue && clientId > 0)
            ClientId = clientId;

        CreatedAt = DateTime.Now;
        ExpiresDate = CreatedAt.AddDays(90);
        Items = [];
        TotalValue();
    }

    public int? ClientId { get; private set; }
    public User? Client { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public DateTime ExpiresDate { get; private set; }
    public decimal SubTotalPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public List<CartItem> Items { get; private set; }

    public void TotalValue()
    {
        if (!Items.Any())
        {
            TotalPrice = 0;
            return;
        }

        SubTotalPrice = Items.Sum(x => x.TotalPrice);
        TotalPrice = Items.Sum(x => x.TotalPrice) + 10;
    }

    public bool AddItem(CartItem item, int stockQuantity)
    {
        var excededStockQuantity = Items.FirstOrDefault(i => i.ProductId == item.ProductId)?.Quantity == stockQuantity;

        if (item is null || excededStockQuantity)
            return false;

        var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);

        if (existingItem != null)
            existingItem.IncreaseQuantity(item.Quantity);
        else
            Items.Add(item);

        TotalValue();
        return true;
    }

    public void DecreaseItem(CartItem item)
    {
        if (item is null)
            return;

        var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);

        if (existingItem != null)
            existingItem.DecreaseQuantity(1);
        else
            Items.Add(item);

        TotalValue();
    }

    public bool RemoveItem(int productId)
    {
        if (productId <= 0)
            return false;

        var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is null)
            return false;

        Items.Remove(existingItem);
        TotalValue();
        return true;
    }

    public bool ValidateExpiresDate() => ExpiresDate < DateTime.Now;
}