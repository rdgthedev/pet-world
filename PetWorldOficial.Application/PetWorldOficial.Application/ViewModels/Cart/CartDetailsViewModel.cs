﻿using PetWorldOficial.Application.ViewModels.CartItem;

namespace PetWorldOficial.Application.ViewModels.Cart
{
    public class CartDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime ExpiresDate { get; set; }
        public List<Domain.Entities.CartItem>? Items { get; set; }
        public int? ClientId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }
    }
}