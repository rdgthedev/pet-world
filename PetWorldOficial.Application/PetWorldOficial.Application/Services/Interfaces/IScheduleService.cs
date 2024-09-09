using System.Security.Cryptography;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.ViewModels.Schedule;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDetailsViewModel>> GetAll(CancellationToken cancellationToken);

    Task<IEnumerable<ScheduleDetailsViewModel?>> GetAllWithEmployeeAndAnimalAndService(
        CancellationToken cancellationToken);

    Task<ScheduleDetailsViewModel?> GetById(int id, CancellationToken cancellationToken);
    Task<ScheduleDetailsViewModel?> GetByIdWithAnimalAndService(int id, CancellationToken cancellationToken);

    Task<List<ScheduleDetailsViewModel?>> GetSchedulesByUsersIdsAsync(IEnumerable<int> usersIds,
        CancellationToken cancellationToken);

    Task<IEnumerable<ScheduleDetailsViewModel>> GetAllByAnimalsIds(IEnumerable<int> id,
        CancellationToken cancellationToken);

    Task<bool> BusySchedule(DateTime date, CancellationToken cancellationToken);
    Task<bool> IsMaximumBookingsExceeded(DateTime date, TimeSpan time, CancellationToken cancellationToken);

    Task<bool> IsMaximumServiceBookingsPerAnimalExceededAsync(
        CreateScheduleCommand command,
        CancellationToken cancellationToken);

    Task Update(UpdateScheduleViewModel model, CancellationToken cancellationToken);
    Task Delete(DeleteScheduleViewModel model, CancellationToken cancellationToken);
    Task Create(CreateScheduleCommand command, CancellationToken cancellationToken);
}