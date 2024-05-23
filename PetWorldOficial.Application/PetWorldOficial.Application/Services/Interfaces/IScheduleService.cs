using PetWorldOficial.Application.DTOs.Schedule.Output;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IScheduleService
{
    public Task<IEnumerable<OutputScheduleDTO>> GetAll();

    public Task<OutputScheduleDTO?> GetById(int id);
    public Task<bool> DateExists(DateTime date);

    public Task Update(Schedule entity);

    public Task Delete(Schedule entity);

    public Task CreateAsync(Schedule schedule);
}