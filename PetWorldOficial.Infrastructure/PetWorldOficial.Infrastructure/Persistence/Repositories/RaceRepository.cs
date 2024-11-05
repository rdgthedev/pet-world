using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetWorldOficial.Infrastructure.Data.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private readonly AppDbContext _context;
        public RaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Race>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context
                .Races
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Race?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context
                .Races
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task CreateAsync(Race race, CancellationToken cancellationToken)
        {
            await _context.Races.AddAsync(race, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Race race, CancellationToken cancellationToken)
        {
            _context.Races.Update(race);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Race race, CancellationToken cancellationToken)
        {
            _context.Races.Remove(race);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
