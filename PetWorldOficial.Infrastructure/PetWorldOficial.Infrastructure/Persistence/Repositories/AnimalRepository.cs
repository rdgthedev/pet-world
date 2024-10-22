using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class AnimalRepository(AppDbContext _context) : IAnimalRepository
{
    public async Task<IEnumerable<Animal?>> GetAllAsync(CancellationToken cancellationToken)
        => await _context
            .Animals
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Animal?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _context
            .Animals
            .AsNoTracking()
            .FirstOrDefaultAsync(animal => animal.Id == id, cancellationToken);

    public async Task CreateAsync(Animal entity, CancellationToken cancellationToken)
    {
        await _context.Animals.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Animal?>> GetByUserIdWithOwnerAndRaceAsync(int id, CancellationToken cancellationToken)
    {
        var animals = await _context
             .Animals
             .AsNoTracking()
             .Include(a => a.Owner)
             .Include(a => a.Race)
             .Where(animal => animal.OwnerId == id)
             .ToListAsync(cancellationToken);

        return animals;
    }

    public async Task<IEnumerable<Animal?>> GetWithOwnerAndRaceAsync(CancellationToken cancellationToken)
    {
        return await _context
            .Animals
            .AsNoTracking()
            .Include(a => a.Owner)
            .Include(a => a.Race)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Animal entity, CancellationToken cancellationToken)
    {
        var animal = _context.Animals.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Animal entity, CancellationToken cancellationToken)
    {
        _context.Animals.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Animal?> GetByIdWithOwnerAndCategoryAndRaceAsync(int id, CancellationToken cancellationToken)
       => await _context
        .Animals
        .AsNoTracking()
        .Include(a => a.Category)
        .Include(a => a.Owner)
        .Include(a => a.Race)
        .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

}