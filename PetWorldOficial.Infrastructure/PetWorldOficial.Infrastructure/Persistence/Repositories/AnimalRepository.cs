using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly AppDbContext _context;

    public AnimalRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Animal>> GetAllAsync()
    {
        return await _context.Animals.AsNoTracking().ToListAsync();
    }
    
    public async Task<Animal?> GetByIdAsync(int id)
    {
        return await _context.Animals.AsNoTracking().FirstOrDefaultAsync(animal => animal.Id == id);
    }
    
    public async Task CreateAsync(Animal entity)
    {
        await _context.Animals.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Animal?> GetByOwnerAsync(int id)
    {
        return await _context
            .Animals
            .AsNoTracking()
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.User.Id == id);
    }

    public async Task Update(Animal entity)
    {
        _context.Animals.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Animal entity)
    {
        _context.Animals.Update(entity);
        await _context.SaveChangesAsync();
    }
}