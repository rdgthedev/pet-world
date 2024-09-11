using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Cart : Entity
{
    public Cart()
    {
    }

    public Cart(int clientId)
    {
        ClientId = clientId;
        CreatedAt = DateTime.Now;
        ExpiresDate = CreatedAt.AddDays(3);
        Items = new();
    }

    public Cart(
        int clientId,
        CartItem cartItem) : this(clientId)
    {
        CreatedAt = DateTime.Now;
        ExpiresDate = CreatedAt.AddDays(3);
        Items.Add(cartItem);
    }


    public int ClientId { get; private set; }
    public User Client { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }
    public DateTime ExpiresDate { get; private set; }
    public decimal TotalPrice { get; set; }
    public List<CartItem> Items { get; private set; }

    private void TotalValue()
    {
        if (!Items.Any())
            return;

        Items.ForEach(ci => TotalPrice += ci.TotalPrice);
    }

    public void AddItem(CartItem item)
    {
        Items.Add(item);
    }
}