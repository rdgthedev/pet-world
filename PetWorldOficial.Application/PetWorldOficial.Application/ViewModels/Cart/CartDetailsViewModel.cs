using PetWorldOficial.Application.ViewModels.CartItem;

namespace PetWorldOficial.Application.ViewModels.Cart
{
    public class CartDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime ExpiresDate { get; set; }
        public List<CartItemDetailsViewModel>? Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}