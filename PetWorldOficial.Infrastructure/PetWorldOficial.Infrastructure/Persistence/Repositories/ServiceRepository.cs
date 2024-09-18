﻿using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class ServiceRepository(AppDbContext context) : IServiceRepository
{
    public async Task<IEnumerable<Service>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context
            .Services
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Service?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context
            .Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
    
    public async Task<Service?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await context
            .Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Name == name, cancellationToken);
    }

    public async Task CreateAsync(Service service, CancellationToken cancellationToken)
    {
        await context.AddAsync(service, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Service service, CancellationToken cancellationToken)
    {
        context.Update(service);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Service service, CancellationToken cancellationToken)
    {
        context.Remove(service);
        await context.SaveChangesAsync(cancellationToken);
    }
}