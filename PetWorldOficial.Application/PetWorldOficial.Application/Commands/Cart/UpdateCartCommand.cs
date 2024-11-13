
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Commands.Cart
{
    public class UpdateCartCommand
    {
        public int Id { get; set; }
        public DateTime ExpiresDate { get; set; }
        public List<Domain.Entities.CartItem>? Items { get; set; }
        public int? ClientId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
