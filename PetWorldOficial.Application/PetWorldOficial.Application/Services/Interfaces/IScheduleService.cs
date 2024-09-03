using System.Security.Cryptography;
using PetWorldOficial.Application.ViewModels.Schedule;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDatailsViewModel>> GetAll(CancellationToken cancellationToken);
    Task<ScheduleDatailsViewModel?> GetById(int id, CancellationToken cancellationToken);
    Task<ScheduleDatailsViewModel?> GetByIdWithAnimalAndService(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ScheduleDatailsViewModel?>> GetAllByAnimalsIds(IEnumerable<int> id, CancellationToken cancellationToken);
    Task<bool> BusySchedule(DateTime date, CancellationToken cancellationToken);
    Task<bool> IsMaximumBookingsExceeded(DateTime date, TimeSpan time, CancellationToken cancellationToken);
    Task<bool> IsMaximumServiceBookingsPerAnimalExceededAsync(CreateScheduleViewModel model, CancellationToken cancellationToken);
    Task Update(UpdateScheduleViewModel model, CancellationToken cancellationToken);
    Task Delete(DeleteScheduleViewModel model, CancellationToken cancellationToken);
    Task Create(CreateScheduleViewModel model, CancellationToken cancellationToken);
}