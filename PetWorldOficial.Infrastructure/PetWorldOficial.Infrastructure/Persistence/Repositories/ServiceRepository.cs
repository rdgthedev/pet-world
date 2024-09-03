using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context) => _context = context;
    
    public async Task<IEnumerable<Service>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context
            .Services
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Service?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context
            .Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
    
    public async Task<Service?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context
            .Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Name == name, cancellationToken);
    }

    public async Task CreateAsync(Service service, CancellationToken cancellationToken)
    {
        await _context.AddAsync(service, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Service service, CancellationToken cancellationToken)
    {
        _context.Update(service);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Service service, CancellationToken cancellationToken)
    {
        _context.Remove(service);
        await _context.SaveChangesAsync(cancellationToken);
    }
}