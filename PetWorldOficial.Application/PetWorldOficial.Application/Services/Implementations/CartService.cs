using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Implementations
{
    public class CartService : ICartService
    {
        public Task<CartDetailsViewModel> CreateAsync(CreateCartCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DeleteCartCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CartDetailsViewModel?> GetByIdAsync(int? id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateCartCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
