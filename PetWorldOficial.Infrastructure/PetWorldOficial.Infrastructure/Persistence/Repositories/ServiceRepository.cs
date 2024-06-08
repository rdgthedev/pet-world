using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context) => _context = context;
    
    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _context.Services.AsNoTracking().ToListAsync();
    }

    public async Task<Service?> GetByIdAsync(int id)
    {
        return await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task<Service?> GetByNameAsync(string name)
    {
        return await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task CreateAsync(Service service)
    {
        await _context.AddAsync(service);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Service service)
    {
        _context.Update(service);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Service service)
    {
        _context.Remove(service);
        await _context.SaveChangesAsync();
    }
}