using PetWorldOficial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Domain.Interfaces.Repositories
{
    public interface IRaceRepository
    {
        Task CreateAsync(Race race, CancellationToken cancellationToken);
        Task<IEnumerable<Race>> GetAllAsync(CancellationToken cancellationToken);
        Task<Race?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Race race, CancellationToken cancellationToken);
        Task DeleteAsync(Race race, CancellationToken cancellationToken);
    }
}
