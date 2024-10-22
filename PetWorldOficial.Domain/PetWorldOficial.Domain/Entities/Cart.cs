using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Cart : Entity
{
    public Cart()
    {
        CreatedAt = DateTime.Now;
        ExpiresDate = CreatedAt.AddDays(3);
        Items = new();
        TotalValue();
    }

    public int ClientId { get; set; }
    public User Client { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public DateTime ExpiresDate { get; private set; }
    public decimal TotalPrice { get; set; }
    public List<CartItem> Items { get; private set; }

    private void TotalValue()
    {
        if (!Items.Any())
            return;

        TotalPrice = Items.Sum(x => x.Price);
    }

    public void AddItem(CartItem? item)
    {
        if (item is null)
            return;

        Items.Add(item);
        TotalValue();
    }

    public bool ValidateExpiresDate() => ExpiresDate < DateTime.Now;
}