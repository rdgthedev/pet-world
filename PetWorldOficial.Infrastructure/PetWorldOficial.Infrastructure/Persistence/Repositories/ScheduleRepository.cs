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

    public async Task<Schedule?> GetByDate(DateTime date)
    {
        return await _context.Schedules.AsNoTracking().FirstOrDefaultAsync(d => d.Date == date);
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