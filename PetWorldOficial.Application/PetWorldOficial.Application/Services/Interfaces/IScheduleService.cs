using PetWorldOficial.Application.DTOs.Schedule.Input;
using PetWorldOficial.Application.DTOs.Schedule.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IScheduleService
{
    public Task<IEnumerable<OutputScheduleDTO>> GetAll();
    public Task<OutputScheduleDTO?> GetById(int id);
    public Task<bool> BusySchedule(DateTime date);
    public Task<bool> MaximumBookingsPerAnimalExceeded(int animalId, DateTime date, TimeSpan time);
    public Task Update(Schedule entity);
    public Task Delete(Schedule entity);
    public Task CreateAsync(RegisterScheduleDTO schedule);
}