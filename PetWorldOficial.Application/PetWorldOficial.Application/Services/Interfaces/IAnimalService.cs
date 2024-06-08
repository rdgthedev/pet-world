using PetWorldOficial.Application.DTOs.Animal.Input;
using PetWorldOficial.Application.DTOs.Animal.Output;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IAnimalService
{
    public Task<IEnumerable<AnimalDetailsViewModel>> GetAll();

    public Task<AnimalDetailsViewModel?> GetById(int id);

    public Task Update(UpdateAnimalViewModel animalDto);

    public Task Delete(DeleteAnimalViewModel animalDto);

    public Task Create(CreateAnimalViewModel animalDto);

    public Task<IEnumerable<AnimalDetailsViewModel?>> GetByUserId(int id);
    public Task<IEnumerable<AnimalDetailsViewModel?>> GetWithUser();
}