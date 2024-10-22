using PetWorldOficial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Domain.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken);
        Task<Cart?> GetByIdAsync(int id);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(Cart cart);
    }
}
