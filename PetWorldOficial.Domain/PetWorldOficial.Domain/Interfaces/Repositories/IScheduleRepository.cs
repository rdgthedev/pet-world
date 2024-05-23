using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    Task CreateAsync(Schedule schedule);
    Task<Schedule?> GetByDate(DateTime date);
}