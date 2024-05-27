using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    Task CreateAsync(Schedule schedule);
    Task<int> GetCountByDate(DateTime date);
    Task<IEnumerable<Schedule?>> GetAllByAnimalIdAndDate(int id, DateTime date);
}