using System.Security.Cryptography;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

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

    Task<ScheduleDetailsViewModel?> GetByAnimalIdAndDateAndTime(
        int animalId,
        DateTime schedulingDate,
        TimeSpan schedulingTime,
        CancellationToken cancellationToken);

    Task<bool> BusySchedule(DateTime date, CancellationToken cancellationToken);
    Task<bool> IsMaximumBookingsExceeded(DateTime date, TimeSpan time, CancellationToken cancellationToken);

    Task<bool> IsMaximumServiceBookingsPerAnimalExceededAsync(
        CreateScheduleCommand command,
        CancellationToken cancellationToken);
    Task<int> CountSchedulesByDateAndHour(DateTime scheduleDate, TimeSpan hour, CancellationToken cancellationToken);

    Task Update(UpdateSchedulingCommand command, CancellationToken cancellationToken);
    Task UpdateRange(List<UpdateSchedulingCommand> schedulings, CancellationToken cancellationToken);
    Task Delete(DeleteScheduleViewModel model, CancellationToken cancellationToken);
    Task DeleteRange(List<DeleteSchedulingCommand> commands, CancellationToken cancellationToken);
    Task Create(CreateScheduleCommand command, CancellationToken cancellationToken);
    Task CreateInBatch(List<CreateScheduleCommand> command, CancellationToken cancellationToken);

    Task<IEnumerable<TimeSpan>> GetAllSchedulesTimesByDateAndCategory(
        DateTime date,
        string category,
        CancellationToken cancellationToken);

    Task<IEnumerable<Schedulling>> GetByCategoryAndDate(
        string category,
        DateTime date,
        CancellationToken cancellationToken);

    Task<IEnumerable<ScheduleDetailsViewModel>> GetByCategoryAndDateAndTime(
        string category,
        DateTime date,
        TimeSpan time,
        CancellationToken cancellationToken);
}