using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<CartDetailsViewModel?> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateCartCommand command, CancellationToken cancellationToken);
        Task<CartDetailsViewModel> CreateAsync(CreateCartCommand command, CancellationToken cancellationToken);
        Task DeleteAsync(DeleteCartCommand command, CancellationToken cancellationToken);
    }
}
