using System.Runtime.InteropServices.JavaScript;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IScheduleRepository
{
    Task CreateAsync(Schedulling schedule, CancellationToken cancellationToken);
    Task<IEnumerable<Schedulling>> GetAllAsync(CancellationToken cancellationToken);
    Task<Schedulling?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Schedulling schedule, CancellationToken cancellationToken);
    Task DeleteAsync(Schedulling schedule, CancellationToken cancellationToken);
    Task<int> GetCountByDateAsync(DateTime date, CancellationToken cancellationToken);
    Task<IEnumerable<Schedulling?>> GetAllByServiceIdAsync(int serviceId, CancellationToken cancellationToken);
    Task<Schedulling?> GetByIdWithAnimalAndService(int id, CancellationToken cancellationToken);

    public Task<IEnumerable<Schedulling?>> GetAllByAnimalsIds(IEnumerable<int> animalIds,
        CancellationToken cancellationToken);

    public Task<IEnumerable<Schedulling?>> GetSchedulesWithEmployeeByDateAndTime(
        DateTime date,
        string serviceName,
        CancellationToken cancellationToken);

    Task<IEnumerable<Schedulling?>> GetAllWithEmployeeAndAnimalAndService(CancellationToken cancellationToken);
    Task<IEnumerable<Schedulling?>> GetSchedulesByUsersIds(IEnumerable<int> usersIds, CancellationToken cancellationToken);
    Task<int> CountSchedulesAsync(DateTime scheduleDate, TimeSpan time, CancellationToken cancellationToken);
}