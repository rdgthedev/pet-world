using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class AnimalRepository(AppDbContext _context) : IAnimalRepository
{
    public async Task<IEnumerable<Animal>> GetAllAsync()
        => await _context.Animals.AsNoTracking().ToListAsync();


    public async Task<Animal?> GetByIdAsync(int id)
        => await _context.Animals.AsNoTracking().FirstOrDefaultAsync(animal => animal.Id == id);


    public async Task CreateAsync(Animal entity)
    {
        await _context.Animals.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Animal?>> GetByUserIdAsync(int id)
    {
        return await _context
            .Animals
            .AsNoTracking()
            .Include(a => a.User)
            .Where(a => a.User.Id == id)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Animal?>> GetWithUser()
    {
        return await _context
            .Animals
            .AsNoTracking()
            .Include(a => a.User)
            .ToListAsync();
    }

    public async Task UpdateAsync(Animal entity)
    {
        var animal = _context.Animals.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Animal entity)
    {
        _context.Animals.Remove(entity);
        await _context.SaveChangesAsync();
    }
}