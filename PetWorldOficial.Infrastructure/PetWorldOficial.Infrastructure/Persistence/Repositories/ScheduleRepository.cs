using System.Xml.Schema;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetWorldOficial.Infrastructure.Context;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class ScheduleRepository(AppDbContext _context) : IScheduleRepository
{
    public async Task<IEnumerable<Schedulling>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .Include(s => s.Service)
            .ToListAsync(cancellationToken);
    }

    public async Task<Schedulling?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task CreateAsync(Schedulling entity, CancellationToken cancellationToken)
    {
        await _context.Schedullings.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetCountByDateAsync(DateTime date, CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .Where(s => s.Date == date)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Schedulling?>> GetAllByServiceIdAsync(int serviceId,
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .Where(s => s.ServiceId == serviceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Schedulling>> GetAllByRoleAndDateAndHour(ERole role, DateTime scheduleDate,
        TimeSpan hour, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Schedulling?>> GetAllWithEmployeeAndAnimalAndService(
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .Include(s => s.Animal)
            .Include(s => s.Service)
            .Include(s => s.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Schedulling?>> GetSchedulesByUsersIds(IEnumerable<int> usersIds,
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .Where(s => usersIds.Contains(s.EmployeeId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TimeSpan>> GetAllScheduleTimesByDate(
        DateTime date,
        IEnumerable<int> employeesIds,
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .Include(s => s.Employee)
            .Where(s => s.Date.Date == date.Date && employeesIds.Contains(s.EmployeeId))
            .Select(s => s.Time)
            .ToListAsync(cancellationToken);
    }

    public async Task<Schedulling?> GetByIdWithAnimalAndService(int id, CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .Include(s => s.Animal)
            .Include(s => s.Service)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Schedulling?>> GetAllByAnimalsIds(IEnumerable<int> animalsIds,
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .Include(schedule => schedule.Service)
            .Include(schedule => schedule.Animal)
            .Where(schedule => animalsIds.Contains(schedule.AnimalId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Schedulling?>> GetSchedulesWithEmployeeByDateAndTime(DateTime date,
        string serviceName,
        CancellationToken cancellationToken)
    {
        return await _context
            .Schedullings
            .AsNoTracking()
            .Include(s => s.Employee)
            .Where(s => s.Date == date && s.Date.Hour == date.Hour)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Schedulling entity, CancellationToken cancellationToken)
    {
        _context.Schedullings.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Schedulling entity, CancellationToken cancellationToken)
    {
        _context.Schedullings.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CountSchedulesAsync(DateTime scheduleDate, TimeSpan time,
        CancellationToken cancellationToken)
    {
        return await _context.Schedullings.Where(s => s.Date == scheduleDate && s.Time == time)
            .CountAsync(cancellationToken);
    }
}