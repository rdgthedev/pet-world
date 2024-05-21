using PetWorldOficial.Application.DTOs.Animal.Input;
using PetWorldOficial.Application.DTOs.Animal.Output;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IAnimalService
{
    public Task<IEnumerable<OutputAnimalDTO>> GetAll();

    public Task<OutputAnimalDTO?> GetById(int id);

    public Task Update(RegisterAnimalDTO animalDto);

    public Task Delete(RegisterAnimalDTO animalDto);

    public Task Create(RegisterAnimalDTO animalDto);

    public Task<OutputAnimalDTO?> GetByOwner(int id);
}