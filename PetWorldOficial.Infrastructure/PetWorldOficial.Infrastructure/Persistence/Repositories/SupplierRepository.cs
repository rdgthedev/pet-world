﻿using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Data.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class SupplierRepository(AppDbContext context) : ISupplierRepository
{
    public async Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context
            .Suppliers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context
            .Suppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Supplier?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await context
            .Suppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Name == name, cancellationToken);
    }

    public async Task<Supplier?> ExistsAsync(string name, string cnpj, string cellPhone,
        CancellationToken cancellationToken)
    {
        return await context
            .Suppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Name == name
                                      || s.CNPJ == cnpj
                                      || s.CellPhone == cellPhone, cancellationToken);
    }

    public async Task CreateAsync(Supplier supplierModel, CancellationToken cancellationToken)
    {
        await context.Suppliers.AddAsync(supplierModel, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Supplier supplierModel, CancellationToken cancellationToken)
    {
        context.Update(supplierModel);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Supplier supplierModel, CancellationToken cancellationToken)
    {
        context.Remove(supplierModel);
        await context.SaveChangesAsync(cancellationToken);
    }
}