using AutoMapper;
using PetWorldOficial.Application.DTOs.Schedule.Input;
using PetWorldOficial.Application.DTOs.Schedule.Output;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public ScheduleService(
        IScheduleRepository scheduleRepository,
        IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<OutputScheduleDTO>> GetAll()
    {
        var schedules = _mapper.Map<IEnumerable<OutputScheduleDTO>>(await _scheduleRepository.GetAllAsync());
        if (schedules == null) throw new NotFoundException("Nenhum agendamento encontrado!");
        return schedules;
    }

    public async Task<OutputScheduleDTO?> GetById(int id)
    {
        var schedule = _mapper.Map<OutputScheduleDTO>(await _scheduleRepository.GetByIdAsync(id));
        if (schedule == null) throw new NotFoundException("Nenhum agendamento encontrado!");
        return schedule;
    }

    public async Task<bool> BusySchedule(DateTime date)
    {
        var countByDate = await _scheduleRepository.GetCountByDate(date);
        return countByDate >= 5;
    }
    
    public async Task<bool> MaximumBookingsPerAnimalExceeded(int animalId, DateTime date, TimeSpan time)
    {
        var schedules = await _scheduleRepository.GetAllByAnimalIdAndDate(animalId, date);
        return schedules.Any(schedule => time.Subtract(schedule!.Time).TotalHours < 1);
    }

    public async Task Update(Schedule entity)
    {
        await _scheduleRepository.Update(entity);
    }

    public async Task Delete(Schedule entity)
    {
        await _scheduleRepository.Delete(entity);
    }

    public async Task CreateAsync(RegisterScheduleDTO registerScheduleDtoscheduleDto)
    {
        var schedule = _mapper.Map<Schedule>(registerScheduleDtoscheduleDto);
        await _scheduleRepository.CreateAsync(schedule);
    }
}