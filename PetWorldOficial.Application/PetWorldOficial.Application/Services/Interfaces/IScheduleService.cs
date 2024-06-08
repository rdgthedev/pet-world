using PetWorldOficial.Application.ViewModels.Schedule;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDatailsViewModel>> GetAll();
    Task<ScheduleDatailsViewModel?> GetById(int id);
    Task<ScheduleDatailsViewModel?> GetByIdWithAnimalAndService(int id);
    Task<IEnumerable<ScheduleDatailsViewModel?>> GetAllByAnimalsIds(IEnumerable<int> id);
    Task<bool> BusySchedule(DateTime date);
    Task<bool> IsMaximumBookingsExceeded(DateTime date, TimeSpan time);
    Task<bool> IsMaximumServiceBookingsPerAnimalExceededAsync(CreateScheduleViewModel model);
    Task Update(UpdateScheduleViewModel model);
    Task Delete(DeleteScheduleViewModel model);
    Task Create(CreateScheduleViewModel model);
}