using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetWorldOficial.Application.ViewModels.CartItem;

namespace PetWorldOficial.Application.Commands.Cart
{
    public class UpdateCartCommand
    {
        public int Id { get; set; }
        public DateTime ExpiresDate { get; set; }
        public List<CartItemDetailsViewModel>? Items { get; set; }
        public int? ClientId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
