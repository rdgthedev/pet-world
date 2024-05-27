using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbContext _context;

    public ScheduleRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Schedule>> GetAllAsync()
    {
        return await _context.Schedules.AsNoTracking().ToListAsync();
    }

    public async Task<Schedule?> GetByIdAsync(int id)
    {
        return await _context.Schedules.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task CreateAsync(Schedule entity)
    {
        await _context.Schedules.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCountByDate(DateTime date)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Where(s => s.Date == date)
            .CountAsync();
    }

    public async Task<IEnumerable<Schedule?>> GetAllByAnimalIdAndDate(int id, DateTime date)
    {
        return await _context
            .Schedules
            .Where(s => s.AnimalId == id && s.Date == date)
            .ToListAsync();
    }

    public async Task Update(Schedule entity)
    {
        _context.Schedules.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Schedule entity)
    {
        _context.Schedules.Remove(entity);
        await _context.SaveChangesAsync();
    }
}