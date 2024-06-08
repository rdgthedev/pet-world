using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class ScheduleRepository(AppDbContext _context) : IScheduleRepository
{
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

    public async Task<int> GetCountByDateAsync(DateTime date)
    {
        return await _context
            .Schedules
            .Where(s => s.Date == date)
            .CountAsync();
    }

    public async Task<IEnumerable<Schedule?>> GetAllByServiceIdAsync(int serviceId)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Where(s => s.ServiceId == serviceId)
            .ToListAsync();
    }

    public async Task<Schedule?> GetByIdWithAnimalAndService(int id)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Include(s => s.Animal)
            .Include(s => s.Service)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Schedule?>> GetAllByAnimalsIds(IEnumerable<int> animalsIds)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Include(schedule => schedule.Service)
            .Include(schedule => schedule.Animal)
            .Where(schedule => animalsIds.Contains(schedule.AnimalId))
            .ToListAsync();
    }

    public async Task UpdateAsync(Schedule entity)
    {
        _context.Schedules.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Schedule entity)
    {
        _context.Schedules.Remove(entity);
        await _context.SaveChangesAsync();
    }
}