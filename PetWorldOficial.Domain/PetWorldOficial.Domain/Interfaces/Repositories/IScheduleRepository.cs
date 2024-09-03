using System.Runtime.InteropServices.JavaScript;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IScheduleRepository
{
    Task CreateAsync(Schedule schedule, CancellationToken cancellationToken);
    Task<IEnumerable<Schedule>> GetAllAsync(CancellationToken cancellationToken);
    Task<Schedule?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Schedule schedule, CancellationToken cancellationToken);
    Task DeleteAsync(Schedule schedule, CancellationToken cancellationToken);
    Task<int> GetCountByDateAsync(DateTime date, CancellationToken cancellationToken);
    Task<IEnumerable<Schedule?>> GetAllByServiceIdAsync(int serviceId, CancellationToken cancellationToken);
    Task<Schedule?> GetByIdWithAnimalAndService(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<Schedule?>> GetAllByAnimalsIds(IEnumerable<int> animalIds, CancellationToken cancellationToken);
}