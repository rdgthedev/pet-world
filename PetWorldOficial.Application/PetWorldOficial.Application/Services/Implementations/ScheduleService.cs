using AutoMapper;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Settings;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class ScheduleService(
    IScheduleRepository _scheduleRepository,
    IMapper _mapper,
    IOptions<OpeningHours> openingHours) : IScheduleService
{
    public async Task<IEnumerable<ScheduleDetailsViewModel>> GetAll(CancellationToken cancellationToken)
        => _mapper.Map<IEnumerable<ScheduleDetailsViewModel>>(await _scheduleRepository.GetAllAsync(cancellationToken));

    public async Task<IEnumerable<ScheduleDetailsViewModel?>> GetAllWithEmployeeAndAnimalAndService(
        CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<ScheduleDetailsViewModel?>>(
            await _scheduleRepository.GetAllWithEmployeeAndAnimalAndService(cancellationToken));
    }

    public async Task<ScheduleDetailsViewModel?> GetById(int id, CancellationToken cancellationToken)
        => _mapper.Map<ScheduleDetailsViewModel>(await _scheduleRepository.GetByIdAsync(id, cancellationToken));

    public async Task<ScheduleDetailsViewModel?> GetByIdWithAnimalAndService(
        int id,
        CancellationToken cancellationToken)
        => _mapper.Map<ScheduleDetailsViewModel>(
            await _scheduleRepository.GetByIdWithAnimalAndService(id, cancellationToken));

    public async Task<List<ScheduleDetailsViewModel?>> GetSchedulesByUsersIdsAsync(IEnumerable<int> usersIds,
        CancellationToken cancellationToken)
    {
        return _mapper.Map<List<ScheduleDetailsViewModel?>>(
            await _scheduleRepository.GetSchedulesByUsersIds(usersIds, cancellationToken));
    }

    public async Task<IEnumerable<ScheduleDetailsViewModel>> GetAllByAnimalsIds(
        IEnumerable<int> ids,
        CancellationToken cancellationToken)
        => _mapper.Map<IEnumerable<ScheduleDetailsViewModel>>(
            await _scheduleRepository.GetAllByAnimalsIds(ids, cancellationToken));

    public async Task<ScheduleDetailsViewModel?> GetByAnimalIdAndDateAndTime(
        int animalId,
        DateTime schedulingDate,
        TimeSpan schedulingTime,
        CancellationToken cancellationToken)
        => _mapper.Map<ScheduleDetailsViewModel>(
            await _scheduleRepository.GetByAnimalIdAndDateAndTime(
                animalId,
                schedulingDate,
                schedulingTime,
                cancellationToken));

    public async Task<bool> BusySchedule(DateTime date,
        CancellationToken cancellationToken)
    {
        var countByDate = await _scheduleRepository.GetCountByDateAsync(date, cancellationToken);
        return countByDate >= 10;
    }

    public async Task<bool> IsMaximumBookingsExceeded(
        DateTime date,
        TimeSpan time,
        CancellationToken cancellationToken)
    {
        var schedules = await _scheduleRepository.GetAllAsync(cancellationToken);

        return schedules.Any(schedule => schedule.Date == date && Math.Abs((schedule.Time - time).TotalHours) < 1);
    }

    public async Task<bool> IsMaximumServiceBookingsPerAnimalExceededAsync(
        CreateScheduleCommand command,
        CancellationToken cancellationToken)
    {
        var schedules = await _scheduleRepository.GetAllByServiceIdAsync(
            command.ServiceId,
            cancellationToken);

        return schedules
            .Any(schedule => schedule!.AnimalId == command.AnimalId && schedule.Date == command.Date);
    }

    public async Task Update(UpdateSchedulingCommand command, CancellationToken cancellationToken)
    {
        var scheduling = await _scheduleRepository.GetByIdAsync(command.SchedulingId, cancellationToken);

        if (scheduling is null)
            throw new Exception();

        scheduling = _mapper.Map(command, scheduling);
        await _scheduleRepository.UpdateAsync(scheduling!, cancellationToken);
    }

    public async Task UpdateRange(List<UpdateSchedulingCommand> commands, CancellationToken cancellationToken)
    {
        var schedulings = await _scheduleRepository.GetByIdsAsync(
            commands.Select(c => c.SchedulingId).ToList(), cancellationToken)!;

        foreach (var command in commands)
        {
            var schedulingToUpdate = schedulings.FirstOrDefault(s => s.Id == command.SchedulingId);

            if (schedulingToUpdate != null)
                _mapper.Map(command, schedulingToUpdate);
        }

        await _scheduleRepository.UpdateRangeAsync(schedulings.ToList(), cancellationToken);
    }

    public async Task Delete(DeleteScheduleViewModel entity, CancellationToken cancellationToken)
        => await _scheduleRepository.DeleteAsync(_mapper.Map<Schedulling>(entity), cancellationToken);

    public async Task Create(CreateScheduleCommand command, CancellationToken cancellationToken)
    {
        var s = new Schedulling(
            command.AnimalId!.Value,
            command.ServiceId,
            command.EmployeeId!.Value,
            command.Date!.Value,
            command.Time!.Value,
            command.Observation);

        await _scheduleRepository.CreateAsync(s, cancellationToken);
    }

    public async Task CreateInBatch(List<CreateScheduleCommand> commands, CancellationToken cancellationToken)
    {
        var schedulings = _mapper.Map<List<Schedulling>>(commands);
        await _scheduleRepository.CreateRangeAsync(schedulings, cancellationToken);
    }

    public async Task<IEnumerable<TimeSpan>> GetAllSchedulesTimesByDateAndCategory(
        DateTime date,
        string category,
        CancellationToken cancellationToken)
        => await _scheduleRepository
            .GetAllScheduleTimesByDateAndCategory(date,
                (ECategoryType)Enum.Parse(typeof(ECategoryType), category),
                cancellationToken);

    public async Task<IEnumerable<Schedulling>> GetByCategoryAndDate(
        string category,
        DateTime date,
        CancellationToken cancellationToken)
        => await _scheduleRepository.GetByCategoryAndDate(
            (ECategoryType)Enum.Parse(typeof(ECategoryType), category),
            date,
            cancellationToken);

    public async Task<IEnumerable<ScheduleDetailsViewModel>> GetByCategoryAndDateAndTime(
        string category,
        DateTime date,
        TimeSpan time,
        CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<ScheduleDetailsViewModel>>(await _scheduleRepository.GetByCategoryAndDateAndTime(
            (ECategoryType)Enum.Parse(typeof(ECategoryType), category),
            date,
            time,
            cancellationToken));
    }

    public async Task<int> CountSchedulesByDateAndHour(DateTime scheduleDate, TimeSpan time,
        CancellationToken cancellationToken)
    {
        return await _scheduleRepository.CountSchedulesAsync(scheduleDate, time, cancellationToken);
    }
}