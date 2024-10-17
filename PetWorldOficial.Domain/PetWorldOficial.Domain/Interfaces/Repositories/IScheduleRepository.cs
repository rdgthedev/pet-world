using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IScheduleRepository
{
    Task CreateAsync(Schedulling schedulling, CancellationToken cancellationToken);
    Task CreateRangeAsync(List<Schedulling> schedullings, CancellationToken cancellationToken);
    Task UpdateRangeAsync(List<Schedulling> schedullings, CancellationToken cancellationToken);
    Task<IEnumerable<Schedulling>> GetAllAsync(CancellationToken cancellationToken);
    Task<Schedulling?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Schedulling>>? GetByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    Task UpdateAsync(Schedulling schedule, CancellationToken cancellationToken);
    Task DeleteAsync(Schedulling schedule, CancellationToken cancellationToken);
    Task DeleteRangeAsync(List<Schedulling> schedulings, CancellationToken cancellationToken);
    Task<int> GetCountByDateAsync(DateTime date, CancellationToken cancellationToken);
    Task<IEnumerable<Schedulling?>> GetAllByServiceIdAsync(int serviceId, CancellationToken cancellationToken);
    Task<Schedulling?> GetByIdWithAnimalAndService(int id, CancellationToken cancellationToken);

    public Task<IEnumerable<Schedulling?>> GetAllByAnimalsIds(IEnumerable<int> animalIds,
        CancellationToken cancellationToken);

    Task<Schedulling?> GetByAnimalIdAndDateAndTime(
        int animalId,
        DateTime schedulingDate,
        TimeSpan schedulingTime,
        CancellationToken cancellationToken);


    public Task<IEnumerable<Schedulling?>> GetSchedulesWithEmployeeByDateAndTime(
        DateTime date,
        string serviceName,
        CancellationToken cancellationToken);

    Task<IEnumerable<Schedulling>> GetAllByRoleAndDateAndHour(ERole role, DateTime scheduleDate, TimeSpan hour,
        CancellationToken cancellationToken);

    Task<IEnumerable<Schedulling?>> GetAllWithEmployeeAndAnimalAndService(CancellationToken cancellationToken);

    Task<IEnumerable<Schedulling?>> GetSchedulesByUsersIds(IEnumerable<int> usersIds,
        CancellationToken cancellationToken);

    Task<IEnumerable<TimeSpan>> GetAllScheduleTimesByDateAndCategory(
        DateTime date,
        ECategoryType categoryType,
        CancellationToken cancellationToken);

    Task<IEnumerable<Schedulling>> GetAllSchedulingsByDateAndTimeAsync(
        DateTime schedulingsDate,
        TimeSpan schedulingTime,
        CancellationToken cancellationToken);

    Task<int> CountSchedulesAsync(DateTime scheduleDate, TimeSpan time, CancellationToken cancellationToken);

    Task<IEnumerable<Schedulling>> GetByCategoryAndDate(
        ECategoryType categoryType,
        DateTime schedullingDate,
        CancellationToken cancellationToken);

    Task<IEnumerable<Schedulling>> GetByCategoryAndDateAndTime(
        ECategoryType categoryType,
        DateTime schedullingDate,
        TimeSpan schedulingTime,
        CancellationToken cancellationToken);
}