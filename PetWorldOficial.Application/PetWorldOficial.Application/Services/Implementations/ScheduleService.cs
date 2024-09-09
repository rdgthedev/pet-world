﻿using AutoMapper;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Settings;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;
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

    public async Task Update(UpdateScheduleViewModel entity, CancellationToken cancellationToken)
        => await _scheduleRepository.UpdateAsync(_mapper.Map<Schedule>(entity), cancellationToken);

    public async Task Delete(DeleteScheduleViewModel entity, CancellationToken cancellationToken)
        => await _scheduleRepository.DeleteAsync(_mapper.Map<Schedule>(entity), cancellationToken);

    public async Task Create(CreateScheduleCommand command, CancellationToken cancellationToken)
    {
        await _scheduleRepository.CreateAsync(_mapper.Map<Schedule>(command), cancellationToken);
    }
}