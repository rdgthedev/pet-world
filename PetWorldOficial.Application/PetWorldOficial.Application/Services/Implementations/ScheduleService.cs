using AutoMapper;
using PetWorldOficial.Application.DTOs.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class ScheduleService(
    IScheduleRepository _scheduleRepository,
    IMapper _mapper) : IScheduleService
{
    public async Task<IEnumerable<ScheduleDatailsViewModel>> GetAll(CancellationToken cancellationToken)
        => _mapper.Map<IEnumerable<ScheduleDatailsViewModel>>(await _scheduleRepository.GetAllAsync(cancellationToken));
    
    public async Task<ScheduleDatailsViewModel?> GetById(int id, CancellationToken cancellationToken)
        => _mapper.Map<ScheduleDatailsViewModel>(await _scheduleRepository.GetByIdAsync(id, cancellationToken));

    public async Task<ScheduleDatailsViewModel?> GetByIdWithAnimalAndService(int id, CancellationToken cancellationToken)
        => _mapper.Map<ScheduleDatailsViewModel>(await _scheduleRepository.GetByIdWithAnimalAndService(id, cancellationToken));

    public async Task<IEnumerable<ScheduleDatailsViewModel?>> GetAllByAnimalsIds(
        IEnumerable<int> ids,
        CancellationToken cancellationToken)
        => _mapper.Map<IEnumerable<ScheduleDatailsViewModel>>(await _scheduleRepository.GetAllByAnimalsIds(ids, cancellationToken));

    public async Task<bool> BusySchedule(DateTime date, CancellationToken cancellationToken)
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

        return schedules
            .Any(schedule => schedule.Date == date &&
                             Math.Abs((schedule.Time - time).TotalHours) < 1);
    }

    public async Task<bool> IsMaximumServiceBookingsPerAnimalExceededAsync(
        CreateScheduleViewModel model,
        CancellationToken cancellationToken)
    {
        var schedules = await _scheduleRepository.GetAllByServiceIdAsync(
            model.ServiceId, 
            cancellationToken);

        return schedules
            .Any(schedule => schedule!.AnimalId == model.AnimalId && schedule.Date == model.Date);
    }

    public async Task Update(UpdateScheduleViewModel entity, CancellationToken cancellationToken)
        => await _scheduleRepository.UpdateAsync(_mapper.Map<Schedule>(entity), cancellationToken);

    public async Task Delete(DeleteScheduleViewModel entity, CancellationToken cancellationToken)
        => await _scheduleRepository.DeleteAsync(_mapper.Map<Schedule>(entity), cancellationToken);

    public async Task Create(CreateScheduleViewModel entity, CancellationToken cancellationToken)
    {
        var schedule = _mapper.Map<Schedule>(entity);
        await _scheduleRepository.CreateAsync(schedule, cancellationToken);
    }
}