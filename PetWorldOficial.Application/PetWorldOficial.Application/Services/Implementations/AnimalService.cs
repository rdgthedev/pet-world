using AutoMapper;
using PetWorldOficial.Application.DTOs.Animal.Input;
using PetWorldOficial.Application.DTOs.Animal.Output;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IMapper _mapper;
    
    public AnimalService(
        IAnimalRepository animalRepository,
        IMapper mapper)
    {
        _animalRepository = animalRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<OutputAnimalDTO>> GetAll()
    {
         var animals = _mapper.Map<IEnumerable<OutputAnimalDTO>>(await _animalRepository.GetAllAsync());
         if (animals == null) throw new NotFoundException("Nenhum animal encontrado!");
         return animals;
    }

    public async Task<OutputAnimalDTO?> GetById(int id)
    {
        var animal = _mapper.Map<OutputAnimalDTO>(await _animalRepository.GetByIdAsync(id));
        if (animal == null) throw new NotFoundException("Animal não encontrado!");
        return animal;
    }

    public async Task Update(RegisterAnimalDTO animalDto)
    {
        await _animalRepository.Update(_mapper.Map<Animal>(animalDto));
    }

    public async Task Delete(RegisterAnimalDTO animalDto)
    {
        await _animalRepository.Delete(_mapper.Map<Animal>(animalDto));
    }

    public async Task Create(RegisterAnimalDTO animalDto)
    {
        var animal = _mapper.Map<Animal>(animalDto);
        await _animalRepository.CreateAsync(animal);
    }

    public async Task<IEnumerable<OutputAnimalDTO?>> GetByOwner(int id)
    {
         var animals = _mapper.Map<IEnumerable<OutputAnimalDTO>>(await _animalRepository.GetByOwnerAsync(id));
         if (animals == null) throw new NotFoundException("Nenhum animal encontrado!");
         return animals;
    }
}