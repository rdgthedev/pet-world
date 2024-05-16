using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context) => _context = context;
    
    public async Task<IEnumerable<Service>> GetAll()
    {
        return await _context.Services.AsNoTracking().ToListAsync();
    }

    public async Task<Service?> GetById(int id)
    {
        return await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task Create(Service service)
    {
        await _context.AddAsync(service);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Service service)
    {
        _context.Update(service);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Service service)
    {
        _context.Remove(service);
        await _context.SaveChangesAsync();
    }
}