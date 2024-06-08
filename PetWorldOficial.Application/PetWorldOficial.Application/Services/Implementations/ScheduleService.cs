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
    public async Task<IEnumerable<ScheduleDatailsViewModel>> GetAll()
        => _mapper.Map<IEnumerable<ScheduleDatailsViewModel>>(await _scheduleRepository.GetAllAsync());
    
    public async Task<ScheduleDatailsViewModel?> GetById(int id)
        => _mapper.Map<ScheduleDatailsViewModel>(await _scheduleRepository.GetByIdAsync(id));

    public async Task<ScheduleDatailsViewModel?> GetByIdWithAnimalAndService(int id)
        => _mapper.Map<ScheduleDatailsViewModel>(await _scheduleRepository.GetByIdWithAnimalAndService(id));

    public async Task<IEnumerable<ScheduleDatailsViewModel?>> GetAllByAnimalsIds(IEnumerable<int> ids)
        => _mapper.Map<IEnumerable<ScheduleDatailsViewModel>>(await _scheduleRepository.GetAllByAnimalsIds(ids));

    public async Task<bool> BusySchedule(DateTime date)
    {
        var countByDate = await _scheduleRepository.GetCountByDateAsync(date);
        return countByDate >= 10;
    }

    public async Task<bool> IsMaximumBookingsExceeded(DateTime date, TimeSpan time)
    {
        var schedules = await _scheduleRepository.GetAllAsync();

        return schedules
            .Any(schedule => schedule.Date == date &&
                             Math.Abs((schedule.Time - time).TotalHours) < 1);
    }

    public async Task<bool> IsMaximumServiceBookingsPerAnimalExceededAsync(CreateScheduleViewModel model)
    {
        var schedules = await _scheduleRepository.GetAllByServiceIdAsync(model.ServiceId);

        return schedules
            .Any(schedule => schedule!.AnimalId == model.AnimalId && schedule.Date == model.Date);
    }

    public async Task Update(UpdateScheduleViewModel entity)
        => await _scheduleRepository.UpdateAsync(_mapper.Map<Schedule>(entity));

    public async Task Delete(DeleteScheduleViewModel entity)
        => await _scheduleRepository.DeleteAsync(_mapper.Map<Schedule>(entity));

    public async Task Create(CreateScheduleViewModel entity)
    {
        var schedule = _mapper.Map<Schedule>(entity);
        await _scheduleRepository.CreateAsync(schedule);
    }
}