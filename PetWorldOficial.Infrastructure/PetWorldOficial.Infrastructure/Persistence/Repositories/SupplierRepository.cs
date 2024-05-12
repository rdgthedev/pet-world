using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;
    
    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _context.Suppliers.AsNoTracking().ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Suppliers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task CreateAsync(Supplier supplierModel)
    {
        await _context.Suppliers.AddAsync(supplierModel);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Supplier supplierModel)
    {
        _context.Update(supplierModel);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Supplier supplierModel)
    {
        _context.Remove(supplierModel);
        await _context.SaveChangesAsync();
    }
}