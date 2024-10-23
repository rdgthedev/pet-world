using AutoMapper;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Cart;
using PetWorldOficial.Application.ViewModels.CartItem;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations
{
    public class CartService(
        ICartRepository cartRepository,
        IMapper mapper) : ICartService
    {
        public async Task<CartDetailsViewModel> CreateAsync(
            CreateCartCommand command,
            CancellationToken cancellationToken)
        {
            var cart = await cartRepository.AddAsync(new Cart(), cancellationToken);

            return new CartDetailsViewModel
            {
                Id = cart.Id,
                ExpiresDate = cart.ExpiresDate,
                Items = cart.Items.Select(x => new CartItemDetailsViewModel
                {
                    CartId = x.CartId,
                    Description = x.Product.Description,
                    ImageUrl = x.Product.ImageUrl,
                    OrderId = x.OrderId,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice
                }).ToList(),
                TotalPrice = cart.TotalPrice
            };
        }

        public Task DeleteAsync(DeleteCartCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDetailsViewModel?> GetByIdAsync(int? id, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByIdAsync(id!.Value, cancellationToken);

            return new CartDetailsViewModel
            {
                Id = cart!.Id,
                ExpiresDate = cart.ExpiresDate,
                Items = cart.Items.Select(x => new CartItemDetailsViewModel
                {
                    CartId = x.CartId,
                    Description = x.Product.Description,
                    ImageUrl = x.Product.ImageUrl,
                    OrderId = x.OrderId,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice
                }).ToList(),
                TotalPrice = cart.TotalPrice
            };
        }

        public Task UpdateAsync(UpdateCartCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}