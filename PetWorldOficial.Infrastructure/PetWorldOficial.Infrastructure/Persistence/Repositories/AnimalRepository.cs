using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Animal?>> GetByUserIdAsync(int id, CancellationToken cancellationToken)
    {
        var animals = await _context
             .Animals
             .AsNoTracking()
             .Include(a => a.User)
             .Where(animal => animal.User.Id == id)
             .ToListAsync(cancellationToken);

        return animals;
    }

    public async Task<IEnumerable<Animal?>> GetWithUser(CancellationToken cancellationToken)
    {
        return await _context
            .Animals
            .AsNoTracking()
            .Include(a => a.User)
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
}