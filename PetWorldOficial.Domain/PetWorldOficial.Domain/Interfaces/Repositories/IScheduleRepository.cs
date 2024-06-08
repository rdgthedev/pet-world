using System.Runtime.InteropServices.JavaScript;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    Task CreateAsync(Schedule schedule);
    Task<int> GetCountByDateAsync(DateTime date);
    Task<IEnumerable<Schedule?>> GetAllByServiceIdAsync(int serviceId);
    Task<Schedule?> GetByIdWithAnimalAndService(int id);
    public Task<IEnumerable<Schedule?>> GetAllByAnimalsIds(IEnumerable<int> animalIds);
}