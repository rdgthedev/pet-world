using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class ScheduleRepository(AppDbContext _context) : IScheduleRepository
{
    public async Task<IEnumerable<Schedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Schedule?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task CreateAsync(Schedule entity, CancellationToken cancellationToken)
    {
        await _context.Schedules.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetCountByDateAsync(DateTime date, CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .Where(s => s.Date == date)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Schedule?>> GetAllByServiceIdAsync(int serviceId, CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Where(s => s.ServiceId == serviceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Schedule?>> GetAllWithEmployeeAndAnimalAndService(CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Include(s => s.Animal)
            .Include(s => s.Service)
            .Include(s => s.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Schedule?>> GetSchedulesByUsersIds(IEnumerable<int> usersIds,
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Where(s => usersIds.Contains(s.EmployeeId))
            .ToListAsync(cancellationToken);
    }

    public async Task<Schedule?> GetByIdWithAnimalAndService(int id, CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Include(s => s.Animal)
            .Include(s => s.Service)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Schedule?>> GetAllByAnimalsIds(IEnumerable<int> animalsIds,
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Include(schedule => schedule.Service)
            .Include(schedule => schedule.Animal)
            .Where(schedule => animalsIds.Contains(schedule.AnimalId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Schedule?>> GetSchedulesWithEmployeeByDateAndTime(DateTime date, string serviceName,
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedules
            .AsNoTracking()
            .Include(s => s.Employee)
            .Where(s => s.Date == date && s.Date.Hour == date.Hour)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Schedule entity, CancellationToken cancellationToken)
    {
        _context.Schedules.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Schedule entity, CancellationToken cancellationToken)
    {
        _context.Schedules.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}