using AutoMapper;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class AnimalService(IAnimalRepository animalRepository, IMapper mapper) : IAnimalService
{
    public async Task<IEnumerable<AnimalDetailsViewModel>> GetAll()
        => mapper.Map<IEnumerable<AnimalDetailsViewModel>>(await animalRepository.GetAllAsync());

    public async Task<AnimalDetailsViewModel?> GetById(int id)
        => mapper.Map<AnimalDetailsViewModel>(await animalRepository.GetByIdAsync(id));
    
    public async Task Update(UpdateAnimalViewModel animalDto)
        => await animalRepository.UpdateAsync(mapper.Map<Animal>(animalDto));
    
    public async Task Delete(DeleteAnimalViewModel animalDto)
        => await animalRepository.DeleteAsync(mapper.Map<Animal>(animalDto));
    
    public async Task Create(CreateAnimalViewModel animalDto)
        => await animalRepository.CreateAsync(mapper.Map<Animal>(animalDto));

    public async Task<IEnumerable<AnimalDetailsViewModel?>> GetByUserId(int id)
        => mapper.Map<IEnumerable<AnimalDetailsViewModel>>(await animalRepository.GetByUserIdAsync(id));

    public async Task<IEnumerable<AnimalDetailsViewModel?>> GetWithUser()
    => mapper.Map<IEnumerable<AnimalDetailsViewModel>>(await animalRepository.GetWithUser());
}